using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.HttpClientBase;

public delegate IHttpClient HttpClientResolverWithToken(string key, string token);
