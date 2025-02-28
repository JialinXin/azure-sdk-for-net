﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Communication.JobRouter.Models;
using Azure.Communication.JobRouter.Tests.Infrastructure;
using Azure.Core.TestFramework;
using NUnit.Framework;

namespace Azure.Communication.JobRouter.Tests.Samples
{
    public class Sample3_SimpleDistributionAsync : SamplesBase<RouterTestEnvironment>
    {
        [Test]
        public async Task SimpleDistribution_LongestIdle()
        {
            JobRouterClient routerClient = new JobRouterClient("<< CONNECTION STRING >>");
            JobRouterAdministrationClient routerAdministrationClient = new JobRouterAdministrationClient("<< CONNECTION STRING >>");

            #region Snippet:Azure_Communication_JobRouter_Tests_Samples_Distribution_LongestIdle
            // In this scenario, we are going to demonstrate how to use the longest idle distribution mode
            // when distributing offers for job to workers
            //
            // We are going to create the following:
            // 1. A single distribution policy with distribution mode set to LongestIdle - we are going to send out only 1 concurrent offer per job
            // 2. A single queue referencing the aforementioned distribution policy
            // 3. Two workers associated with the queue, registering themselves are different timestamps
            // 4. A job created with manual queueing
            //
            // We will observe the following:-
            // Worker1 (who had registered earlier) will get the offer for the job

            // Create distribution policy
            string distributionPolicyId = "distribution-policy-id-5";
            Response<DistributionPolicy> distributionPolicy = await routerAdministrationClient.CreateDistributionPolicyAsync(
                options: new CreateDistributionPolicyOptions(distributionPolicyId: distributionPolicyId, offerExpiresAfter: TimeSpan.FromMinutes(5), mode: new LongestIdleMode()) { Name = "Simple longest idle" });

            // Create queue
            string queueId = "queue-id-1";
            Response<Models.RouterQueue> jobQueue = await routerAdministrationClient.CreateQueueAsync(new CreateQueueOptions(
                queueId: queueId,
                distributionPolicyId: distributionPolicyId));

            // Setting up 2 identical workers
            string worker1Id = "worker-id-1";
            string worker2Id = "worker-id-2";

            Response<RouterWorker> worker1 = await routerClient.CreateWorkerAsync(
                options: new CreateWorkerOptions(workerId: worker1Id, totalCapacity: 10)
                {
                    ChannelConfigurations = { ["general"] = new ChannelConfiguration(10), },
                    QueueAssignments = { [queueId] = new RouterQueueAssignment(), },
                });

            Response<RouterWorker> worker2 = await routerClient.CreateWorkerAsync(
                options: new CreateWorkerOptions(workerId: worker2Id, totalCapacity: 10)
                {
                    ChannelConfigurations = { ["general"] = new ChannelConfiguration(10), },
                    QueueAssignments = { [queueId] = new RouterQueueAssignment(), },
                });

            // Register worker1
            worker1 = await routerClient.UpdateWorkerAsync(new UpdateWorkerOptions(worker1Id) { AvailableForOffers = true });

            // wait for 5 seconds to simulate worker 1 has been idle longer
            await Task.Delay(TimeSpan.FromSeconds(5));

            // Register worker2
            worker2 = await routerClient.UpdateWorkerAsync(new UpdateWorkerOptions(worker2Id) { AvailableForOffers = true });

            // Create a job
            string jobId = "job-id-1";
            Response<RouterJob> job = await routerClient.CreateJobAsync(new CreateJobOptions(jobId: jobId, channelId: "general", queueId: queueId));

#if !SNIPPET
            bool condition = false;
            DateTimeOffset startTime = DateTimeOffset.UtcNow;
            TimeSpan maxWaitTime = TimeSpan.FromSeconds(10);
            while (!condition && DateTimeOffset.UtcNow.Subtract(startTime) <= maxWaitTime)
            {
                Response<RouterWorker> worker1Dto = await routerClient.GetWorkerAsync(worker1Id);
                condition = worker1Dto.Value.Offers.Any(offer => offer.JobId == jobId);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
#endif

            Response<RouterWorker> queriedWorker1 = await routerClient.GetWorkerAsync(worker1Id);
            Response<RouterWorker> queriedWorker2 = await routerClient.GetWorkerAsync(worker2Id);

            // Worker1 would have got the first offer
            Console.WriteLine($"Worker 1 has successfully received offer for job: {queriedWorker1.Value.Offers.Any(offer => offer.JobId == jobId)}"); // true
            Console.WriteLine($"Worker 2 has not been issued an offer for job: {queriedWorker2.Value.Offers.Any(offer => offer.JobId == jobId)}");  // false

            #endregion Snippet:Azure_Communication_JobRouter_Tests_Samples_Distribution_LongestIdle
        }

        [Test]
        public async Task SimpleDistribution_RoundRobin()
        {
            JobRouterClient routerClient = new JobRouterClient("<< CONNECTION STRING >>");
            JobRouterAdministrationClient routerAdministrationClient = new JobRouterAdministrationClient("<< CONNECTION STRING >>");

            #region Snippet:Azure_Communication_JobRouter_Tests_Samples_Distribution_RoundRobin
            // In this scenario, we are going to demonstrate how to use the round robin distribution mode
            // when distributing offers for job to workers
            //
            // We are going to create the following:
            // 1. A single distribution policy with distribution mode set to RoundRobin - we are going to send out only 1 concurrent offer per job
            // 2. A single queue referencing the aforementioned distribution policy
            // 3. Two identical workers associated with the queue
            // 4. Two identical jobs created with manual queueing
            //
            // We will observe the following:-
            // Worker1 will get the offer for job1
            // Worker2 will get the offer for job2

            // Create distribution policy
            string distributionPolicyId = "distribution-policy-id-6";
            Response<DistributionPolicy> distributionPolicy = await routerAdministrationClient.CreateDistributionPolicyAsync(
                options: new CreateDistributionPolicyOptions(
                    distributionPolicyId: distributionPolicyId,
                    offerExpiresAfter: TimeSpan.FromMinutes(5),
                    mode: new RoundRobinMode()) { Name = "Simple round robin" });

            // Create queue
            string queueId = "queue-id-1";
            Response<Models.RouterQueue> jobQueue = await routerAdministrationClient.CreateQueueAsync(new CreateQueueOptions(queueId: queueId, distributionPolicyId: distributionPolicyId));

            // Setting up 2 identical workers
            string worker1Id = "worker-id-1";
            string worker2Id = "worker-id-2";

            Response<RouterWorker> worker1 = await routerClient.CreateWorkerAsync(
                options: new CreateWorkerOptions(workerId: worker1Id, totalCapacity: 10)
                {
                    ChannelConfigurations = { ["general"] = new ChannelConfiguration(5), },
                    QueueAssignments = { [queueId] = new RouterQueueAssignment(), },
                    AvailableForOffers = true
                });

            Response<RouterWorker> worker2 = await routerClient.CreateWorkerAsync(
                options: new CreateWorkerOptions(workerId: worker2Id, totalCapacity: 10)
                {
                    ChannelConfigurations = { ["general"] = new ChannelConfiguration(5), },
                    QueueAssignments = { [queueId] = new RouterQueueAssignment(), },
                    AvailableForOffers = true, // register worker upon creation
                });

            // Setting up two identical jobs
            string job1Id = "job-id-1";
            string job2Id = "job-id-2";

            Response<RouterJob> job1 = await routerClient.CreateJobAsync(new CreateJobOptions(jobId: job1Id, channelId: "general", queueId: queueId));
            Response<RouterJob> job2 = await routerClient.CreateJobAsync(new CreateJobOptions(jobId: job2Id, channelId: "general", queueId: queueId));

#if !SNIPPET
            bool condition = false;
            DateTimeOffset startTime = DateTimeOffset.UtcNow;
            TimeSpan maxWaitTime = TimeSpan.FromSeconds(10);
            while (!condition && DateTimeOffset.UtcNow.Subtract(startTime) <= maxWaitTime)
            {
                Response<RouterWorker> worker1Dto = await routerClient.GetWorkerAsync(worker1Id);
                Response<RouterWorker> worker2Dto = await routerClient.GetWorkerAsync(worker2Id);
                condition = worker1Dto.Value.Offers.Any(offer => offer.JobId == job1Id)
                            && worker2Dto.Value.Offers.Any(offer => offer.JobId == job2Id);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
#endif

            Response<RouterWorker> queriedWorker1 = await routerClient.GetWorkerAsync(worker1Id);
            Response<RouterWorker> queriedWorker2 = await routerClient.GetWorkerAsync(worker2Id);

            // Worker1 would have got the first offer
            Console.WriteLine($"Worker 1 has successfully received offer for job1: {queriedWorker1.Value.Offers.Any(offer => offer.JobId == job1Id)}"); // true
            Console.WriteLine($"Worker 2 has successfully received offer for job2: {queriedWorker2.Value.Offers.Any(offer => offer.JobId == job2Id)}");  // true

            #endregion Snippet:Azure_Communication_JobRouter_Tests_Samples_Distribution_RoundRobin
        }

        [Test]
        public async Task SimpleDistribution_DefaultBestWorker()
        {
            JobRouterClient routerClient = new JobRouterClient("<< CONNECTION STRING >>");
            JobRouterAdministrationClient routerAdministrationClient = new JobRouterAdministrationClient("<< CONNECTION STRING >>");

            #region Snippet:Azure_Communication_JobRouter_Tests_Samples_Distribution_DefaultBestWorker
            // In this scenario, we are going to demonstrate how to use the default best worker distribution mode
            // when distributing offers for job to workers
            //
            // The default scoring formula uses the number of job labels that the worker
            // labels match, as well as the number of label selectors the worker labels match and/or exceed
            // using a logistic function (https://en.wikipedia.org/wiki/Logistic_function).
            //
            // A more detailed documentation can be found here:
            // https://docs.microsoft.com/azure/communication-services/concepts/router/distribution-concepts#default-label-matching
            // https://docs.microsoft.com/azure/communication-services/concepts/router/distribution-concepts#default-worker-selector-matching
            //
            // We are going to create the following:
            // 1. A single distribution policy with distribution mode set to BestWorker - we are going to send out only 1 concurrent offer per job
            // 2. A single queue referencing the aforementioned distribution policy
            // 3. Three workers associated with the queue - each with different sets of labels
            // 4. A single job created with manual queueing, with a set of labels and worker selectors
            //
            // We will observe the following:-
            // Worker1 will not get the offer for the job (complete label overlap + partial worker selector matched)
            // Worker2 will get the first offer for the job (complete label overlap + complete worker selector matched)
            // Worker3 will not get the offer for the job (partial label overlap + partial worker selector match)

            // Create distribution policy
            string distributionPolicyId = "distribution-policy-id-7";
            Response<DistributionPolicy> distributionPolicy = await routerAdministrationClient.CreateDistributionPolicyAsync(
                options: new CreateDistributionPolicyOptions(
                    distributionPolicyId: distributionPolicyId,
                    offerExpiresAfter: TimeSpan.FromMinutes(5),
                    mode: new BestWorkerMode()) { Name = "Default best worker mode" });

            // Create queue
            string queueId = "queue-id-1";
            Response<Models.RouterQueue> jobQueue = await routerAdministrationClient.CreateQueueAsync(new CreateQueueOptions(
                queueId: queueId,
                distributionPolicyId: distributionPolicyId));

            // Setting up 3 workers with different labels
            string worker1Id = "worker-id-1";
            string worker2Id = "worker-id-2";
            string worker3Id = "worker-id-3";

            Response<RouterWorker> worker1 = await routerClient.CreateWorkerAsync(
                options: new CreateWorkerOptions(workerId: worker1Id, totalCapacity: 10)
                {
                    ChannelConfigurations = { ["general"] = new ChannelConfiguration(10), },
                    QueueAssignments = { [queueId] = new RouterQueueAssignment(), },
                    Labels =
                    {
                        ["Location"] = new LabelValue("United States"),
                        ["Language"] = new LabelValue("en-us"),
                        ["Region"] = new LabelValue("NA"),
                        ["Hardware_Support"] = new LabelValue(true),
                        ["Hardware_Support_SurfaceLaptop"] = new LabelValue(true),
                        ["Language_Skill_Level_EN_US"] = new LabelValue(10),
                    }
                });

            Response<RouterWorker> worker2 = await routerClient.CreateWorkerAsync(
                options: new CreateWorkerOptions(workerId: worker2Id, totalCapacity: 10)
                {
                    ChannelConfigurations = { ["general"] = new ChannelConfiguration(10), },
                    QueueAssignments = { [queueId] = new RouterQueueAssignment(), },
                    Labels =
                    {
                        ["Location"] = new LabelValue("United States"),
                        ["Language"] = new LabelValue("en-us"),
                        ["Region"] = new LabelValue("NA"),
                        ["Hardware_Support"] = new LabelValue(true),
                        ["Hardware_Support_SurfaceLaptop"] = new LabelValue(true),
                        ["Language_Skill_Level_EN_US"] = new LabelValue(20),
                    }
                });

            Response<RouterWorker> worker3 = await routerClient.CreateWorkerAsync(
                options: new CreateWorkerOptions(workerId: worker3Id, totalCapacity: 10)
                {
                    ChannelConfigurations = { ["general"] = new ChannelConfiguration(10), },
                    QueueAssignments = { [queueId] = new RouterQueueAssignment(), },
                    Labels =
                    {
                        ["Location"] = new LabelValue("United States"),
                        ["Language"] = new LabelValue("en-us"),
                        ["Region"] = new LabelValue("NA"),
                        ["Hardware_Support"] = new LabelValue(true),
                        ["Hardware_Support_SurfaceLaptop"] = new LabelValue(false),
                        ["Language_Skill_Level_EN_US"] = new LabelValue(1),
                    }
                });

            string jobId = "job-id-1";
            Response<RouterJob> job = await routerClient.CreateJobAsync(
                options: new CreateJobOptions(jobId: jobId, channelId: "general", queueId: queueId)
                {
                    Labels =
                    {
                        ["Location"] = new LabelValue("United States"),
                        ["Language"] = new LabelValue("en-us"),
                        ["Region"] = new LabelValue("NA"),
                        ["Hardware_Support"] = new LabelValue(true),
                        ["Hardware_Support_SurfaceLaptop"] = new LabelValue(true),
                    },
                    RequestedWorkerSelectors =
                    {
                        new RouterWorkerSelector("Language_Skill_Level_EN_US", LabelOperator.GreaterThanEqual, new LabelValue(0)),
                    }
                });

#if !SNIPPET
            bool condition = false;
            DateTimeOffset startTime = DateTimeOffset.UtcNow;
            TimeSpan maxWaitTime = TimeSpan.FromSeconds(10);
            while (!condition && DateTimeOffset.UtcNow.Subtract(startTime) <= maxWaitTime)
            {
                Response<RouterWorker> worker2Dto = await routerClient.GetWorkerAsync(worker2Id);
                condition = worker2Dto.Value.Offers.Any(offer => offer.JobId == jobId);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
#endif

            Response<RouterWorker> queriedWorker1 = await routerClient.GetWorkerAsync(worker1Id);
            Response<RouterWorker> queriedWorker2 = await routerClient.GetWorkerAsync(worker2Id);
            Response<RouterWorker> queriedWorker3 = await routerClient.GetWorkerAsync(worker3Id);

            // Worker1 would have got the first offer
            Console.WriteLine($"Worker 1 has not received an offer for job: {queriedWorker1.Value.Offers.All(offer => offer.JobId != jobId)}"); // true
            Console.WriteLine($"Worker 2 has successfully received offer for job: {queriedWorker2.Value.Offers.Any(offer => offer.JobId == jobId)}");  // true
            Console.WriteLine($"Worker 3 has not received an offer for job: {queriedWorker3.Value.Offers.All(offer => offer.JobId != jobId)}"); // true

            #endregion Snippet:Azure_Communication_JobRouter_Tests_Samples_Distribution_DefaultBestWorker
        }

        [Test]
        public async Task SimpleDistribution_ConcurrentOffers_ModeAgnostic()
        {
            JobRouterClient routerClient = new JobRouterClient("<< CONNECTION STRING >>");
            JobRouterAdministrationClient routerAdministrationClient = new JobRouterAdministrationClient("<< CONNECTION STRING >>");

            #region Snippet:Azure_Communication_JobRouter_Tests_Samples_Distribution_ConcurrentOffers_ModeAgnostic
            // In this scenario, we are going to demonstrate how to set up concurrent offers so that multiple workers
            // can received offers for the same job in parallel.
            //
            // We are going to create the following:
            // 1. A single distribution policy with distribution mode set to LongestIdle - we are going to send out only 2 concurrent offers per job
            // 2. A single queue referencing the aforementioned distribution policy
            // 3. Two workers associated with the queue
            // 4. A job created with manual queueing
            //
            // We will observe the following:-
            // Both Worker1 and Worker2 will get offer for the job

            // Create distribution policy
            string distributionPolicyId = "distribution-policy-id-8";
            Response<DistributionPolicy> distributionPolicy =
                await routerAdministrationClient.CreateDistributionPolicyAsync(
                    options: new CreateDistributionPolicyOptions(
                        distributionPolicyId: distributionPolicyId,
                        offerExpiresAfter: TimeSpan.FromMinutes(5),
                        mode: new LongestIdleMode { MinConcurrentOffers = 1, MaxConcurrentOffers = 2 })
                    {
                        Name = "Simple longest idle"
                    });

            // Create queue
            string queueId = "queue-id-1";
            Response<Models.RouterQueue> jobQueue = await routerAdministrationClient.CreateQueueAsync(new CreateQueueOptions(
                queueId: queueId,
                distributionPolicyId: distributionPolicyId));

            // Setting up 2 identical workers
            string worker1Id = "worker-id-1";
            string worker2Id = "worker-id-2";

            Response<RouterWorker> worker1 = await routerClient.CreateWorkerAsync(
                options: new CreateWorkerOptions(workerId: worker1Id, totalCapacity: 10)
                {
                    ChannelConfigurations = { ["general"] = new ChannelConfiguration(10), },
                    QueueAssignments = { [queueId] = new RouterQueueAssignment(), },
                    AvailableForOffers = true,
                });

            Response<RouterWorker> worker2 = await routerClient.CreateWorkerAsync(
                options: new CreateWorkerOptions(workerId: worker2Id, totalCapacity: 10)
                {
                    ChannelConfigurations = { ["general"] = new ChannelConfiguration(10), },
                    QueueAssignments = { [queueId] = new RouterQueueAssignment(), },
                    AvailableForOffers = true,
                });

            // Create a job
            string jobId = "job-id-1";
            Response<RouterJob> job = await routerClient.CreateJobAsync(new CreateJobOptions(jobId: jobId, channelId: "general", queueId: queueId));

#if !SNIPPET
            bool condition = false;
            DateTimeOffset startTime = DateTimeOffset.UtcNow;
            TimeSpan maxWaitTime = TimeSpan.FromSeconds(10);
            while (!condition && DateTimeOffset.UtcNow.Subtract(startTime) <= maxWaitTime)
            {
                Response<RouterWorker> worker1Dto = await routerClient.GetWorkerAsync(worker1Id);
                condition = worker1Dto.Value.Offers.Any(offer => offer.JobId == jobId);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
#endif

            Response<RouterWorker> queriedWorker1 = await routerClient.GetWorkerAsync(worker1Id);
            Response<RouterWorker> queriedWorker2 = await routerClient.GetWorkerAsync(worker2Id);

            // Worker1 would have got the first offer
            Console.WriteLine($"Worker 1 has successfully received offer for job: {queriedWorker1.Value.Offers.Any(offer => offer.JobId == jobId)}"); // true
            Console.WriteLine($"Worker 2 has successfully received offer for job: {queriedWorker2.Value.Offers.Any(offer => offer.JobId == jobId)}"); // true
            #endregion Snippet:Azure_Communication_JobRouter_Tests_Samples_Distribution_ConcurrentOffers_ModeAgnostic
        }
    }
}
