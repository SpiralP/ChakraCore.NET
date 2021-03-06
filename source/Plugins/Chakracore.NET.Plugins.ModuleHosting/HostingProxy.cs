﻿using ChakraCore.NET;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChakraCore.NET.Hosting;
namespace ChakraCore.NET.Plugin.ModuleHosting
{
    public class HostingProxy:JSClassWrapperBase
    {
        public Task<string> Dispatch(string functionName, string JSONparameter)
        {
            return Task.Factory.StartNew(() =>
            {
                var x = Reference.CallFunction<string, string, string>("__Dispatch__", functionName, JSONparameter);
                return x;
            });

        }

        /// <summary>
        /// Do not use, still have potencial thread conflict issue, may cause application crash
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="JSONparameter"></param>
        /// <returns></returns>
        public Task<string> DispatchAsync(string functionName, string JSONparameter)
        {
            //
            var t = Task.Factory.StartNew(() =>
            {
                var x = Reference.CallFunctionAsync<string, string, string>("DispatchAsync", functionName, JSONparameter);
                x.Wait(); //force wait on caller thread, otherwise may cause chakracontext thread confilict
                return x.Result;
            });
            return t;
        }
    }
}
