﻿using Kentor.AuthServices;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Sustainsys.Saml2.AspNetCore2
{
    class PostConfigureSaml2Options : IPostConfigureOptions<Saml2Options>
    {
        private ILoggerFactory loggerFactory;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loggerFactory">Logger factory to use to hook up Saml2 loggin.</param>
        public PostConfigureSaml2Options(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public void PostConfigure(string name, Saml2Options options)
        {
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if(loggerFactory != null)
            {
                options.SPOptions.Logger = new AspNetCoreLoggerAdapter(
                    loggerFactory.CreateLogger<Saml2Handler>());
            }
            else
            {
                options.SPOptions.Logger = new NullLoggerAdapter();
            }
            options.SPOptions.Logger.WriteVerbose("Saml2 logging enabled.");
        }
    }
}
