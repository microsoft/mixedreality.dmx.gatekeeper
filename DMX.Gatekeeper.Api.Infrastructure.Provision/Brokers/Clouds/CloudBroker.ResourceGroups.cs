using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace DMX.Gatekeeper.Api.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker : ICloudBroker
    {
        public async ValueTask<bool> CheckResourceGroupExistsAsync(string resourceGroupName) =>
            await this.azure.ResourceGroups.ContainAsync(resourceGroupName);

        public async ValueTask<IResourceGroup> CreateResourceGroupAsync(
            string resourceGroupName)
        {
            return await this.azure.ResourceGroups
                .Define(resourceGroupName)
                .WithRegion(Region.USWest)
                .CreateAsync();
        }

        public async ValueTask DeleteResourceGroupAsync(string resourceGroupName) =>
            await this.azure.ResourceGroups.DeleteByNameAsync(resourceGroupName);
    }
}