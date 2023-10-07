﻿// This source code is dual-licensed under the Apache License, version
// 2.0, and the Mozilla Public License, version 2.0.
//
// The APL v2.0:
//
//---------------------------------------------------------------------------
//   Copyright (c) 2007-2020 VMware, Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       https://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//---------------------------------------------------------------------------
//
// The MPL v2.0:
//
//---------------------------------------------------------------------------
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
//
//  Copyright (c) 2007-2020 VMware, Inc.  All rights reserved.
//---------------------------------------------------------------------------

using System;
using System.IO;

namespace RabbitMQ.Client.Unit
{
    public class SslEnv
    {
        private readonly string _certPassphrase;
        private readonly string _certPath;
        private readonly string _hostname;
        private readonly string _sslDir;
        private readonly bool _isSslConfigured;
        private readonly bool _isGithubActions;

        public SslEnv()
        {
            _sslDir = Environment.GetEnvironmentVariable("SSL_CERTS_DIR");
            _certPassphrase = Environment.GetEnvironmentVariable("PASSWORD");

            Boolean.TryParse(Environment.GetEnvironmentVariable("GITHUB_ACTIONS"), out _isGithubActions);

            _isSslConfigured = Directory.Exists(_sslDir) &&
                (false == String.IsNullOrEmpty(_certPassphrase));

            if (_isGithubActions)
            {
                _hostname = "localhost";
            }
            else
            {
                _hostname = System.Net.Dns.GetHostName();
            }

            _certPath = Path.Combine(_sslDir, $"client_{_hostname}.p12");
        }

        public string CertPath
        {
            get { return _certPath; }
        }

        public string CertPassphrase
        {
            get { return _certPassphrase; }
        }

        public string Hostname
        {
            get { return _hostname; }
        }

        public bool IsSslConfigured
        {
            get { return _isSslConfigured; }
        }

        public bool IsGitHubActions
        {
            get { return _isGithubActions; }
        }
    }
}
