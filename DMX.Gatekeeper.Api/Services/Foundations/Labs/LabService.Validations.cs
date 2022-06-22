// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Gatekeeper.Api.Models.Labs;
using DMX.Gatekeeper.Api.Models.Labs.Exceptions;

namespace DMX.Gatekeeper.Api.Services.Foundations.Labs
{
    public partial class LabService
    {
        private void ValidateLab(Lab lab)
        {
            if (lab is null)
            {
                throw new NullLabException();
            }
        }
    }
}
