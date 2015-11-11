using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class counter
    {
        private string group;
        private int count = 0;
        private int nroCount = 0;
        public void setGroup(string s)
        {
            group = s;
        }
        public string getGroup()
        {
            return group;
        }
        public void setCount()
        {
            count++;
        }
        public int getCount()
        {
            return count;
        }
        public void setNroCounts()
        {
            nroCount++;
        }
        public int getNroCounts()
        {
            return nroCount;
        }
    }

}