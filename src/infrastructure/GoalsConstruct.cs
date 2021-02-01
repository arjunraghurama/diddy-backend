﻿using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Cognito;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Infrastructure.Goals;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class GoalsConstruct : Construct
    {
        public GoalsConstruct(Construct scope, string id, Amazon.CDK.AWS.APIGateway.Resource apiParent)
            : base(scope, id)
        {
            var dynamoDb = new DynamoDbResources(scope, id);
            var lambdaResources = new LambdaResources(scope, id);
            var apiGatewayResources = new ApiGatewayResources(scope, id, apiParent, lambdaResources);
            
            dynamoDb.GoalsTable.GrantReadWriteData(lambdaResources.AddGoal);
            dynamoDb.GoalsTable.GrantReadData(lambdaResources.GetGoal);
            dynamoDb.GoalsTable.GrantReadWriteData(lambdaResources.DeleteGoal);
            dynamoDb.GoalsTable.GrantReadWriteData(lambdaResources.UpdateGoal);
        }
    }
}