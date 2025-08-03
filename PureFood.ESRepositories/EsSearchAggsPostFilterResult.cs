﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ESRepositories
{
    public class EsSearchAggsPostFilterResult<T>
    {
        public ObjectSearchResult<T>? hits { get; set; }
        public T _source { get; set; }
        public Aggregations? aggregations { get; set; }

        public class Aggregations
        {
            public NewsStatus news_status { get; set; }
            //public NewsIsReturn news_isreturn { get; set; }
            //public NewsIsTimer news_istimer { get; set; }
            public NewsAggsByStatus news_isreturn { get; set; }
            public NewsAggsByStatus news_istimer { get; set; }
            public NewsAggsByStatus news_publish { get; set; }
            public NewsTotalView news_totalview { get; set; }
            public NewsTotalGa news_totalga { get; set; }
            public class NewsTotalView
            {
                public int doc_count { get; set; }
                public newsTotalViewResult news_totalview { get; set; }
            }
            public class NewsTotalGa
            {
                public int doc_count { get; set; }
                public newsTotalViewResult news_totalview { get; set; }
            }
            public class newsTotalViewResult
            {
                public float value { get; set; }
            }
            public class NewsStatus
            {
                public int doc_count_error_upper_bound { get; set; }
                public int sum_other_doc_count { get; set; }
                public Bucket[] buckets { get; set; }
            }
            public class Bucket
            {
                public int key { get; set; }
                public int doc_count { get; set; }
            }

            public class NewsIsReturn
            {
                public int doc_count { get; set; }
            }

            public class NewsIsTimer
            {
                public int doc_count { get; set; }
            }

            public class NewsAggsByStatus
            {
                public int doc_count { get; set; }
            }
        }
    }
}
