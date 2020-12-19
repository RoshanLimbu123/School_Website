using CMS.Core.Exceptions;
using CMS.Core.Helper;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Service.Implementation
{
    public class SlugGeneratorImpl : SlugGenerator
    {
        public string generate(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new NonEmptyValueException("Slug title is required.");
            }
            title = title.ToLower();
            title= title.Replace(" ", "-");
            title = title + "-" + DateTime.Now.Ticks;
            return title;

            //var allIndexOfOccurenceOfDash = title.AllIndexesOf("-").ToList();

            //List<int> consecutiveOccuranceIndex = new List<int>();
            //int nextIndex = 1;
            //for(int i = 0; i < allIndexOfOccurenceOfDash.Count; i++)
            //{
            //    if (nextIndex != allIndexOfOccurenceOfDash.Count)
            //    {
            //        if(allIndexOfOccurenceOfDash[i]== allIndexOfOccurenceOfDash[nextIndex])
            //        {
            //            consecutiveOccuranceIndex.Add(i);
            //        }
            //    }
            //    nextIndex++;
            //}
            //foreach(int consecutiveIndex in consecutiveOccuranceIndex)
            //{
               
            //}
        }
    }
}
