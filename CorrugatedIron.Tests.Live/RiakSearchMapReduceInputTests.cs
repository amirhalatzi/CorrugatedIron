// Copyright (c) 2011 - OJ Reeves & Jeremiah Peschka
//
// This file is provided to you under the Apache License,
// Version 2.0 (the "License"); you may not use this file
// except in compliance with the License.  You may obtain
// a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.

using System;
using System.Linq;
using CorrugatedIron.Models;
using CorrugatedIron.Models.MapReduce;
using CorrugatedIron.Tests.Extensions;
using CorrugatedIron.Extensions;
using CorrugatedIron.Tests.Live.LiveRiakConnectionTests;
using NUnit.Framework;

namespace CorrugatedIron.Tests.Live
{
    [TestFixture()]
    public class RiakSearchMapReduceInputTests : RiakMapReduceTests
    {
        // N.B. You need to install the search hooks on the riak_search_bucket first via `bin/search-cmd install riak_search_bucket`
        private string _riakSearchKey2 = "a.public";
        private string _riakSearchDoc2 = "{\"name\":\"Alan Q. Public\", \"bio\":\"I'm an exciting mathematician\", \"favorites\":{\"book\":\"Prelude to Mathematics\",\"album\":\"The Fame Monster\"}}";
        public RiakSearchMapReduceInputTests () : base()
        {
            bucket = "riak_search_bucket";
        }
        
        [SetUp]
        public void SetUp() 
        {
            Cluster = new RiakCluster(ClusterConfig, new RiakConnectionFactory());
            ClientGenerator = () => new RiakClient(Cluster);
            Client = ClientGenerator();
        }
        
        [TearDown]
        public void TearDown()
        {
            Client.DeleteBucket(bucket);
        }
            Client.Put(new RiakObject(bucket, _riakSearchKey2, _riakSearchDoc2, RiakConstants.ContentTypes.ApplicationJson));
            
            var mrResult = result.Value;
            mrResult.PhaseResults.Count().ShouldEqual(3);
            
            mrResult.PhaseResults.ElementAt(0).Value.FromRiakString().ShouldEqual("12");
    }
}
