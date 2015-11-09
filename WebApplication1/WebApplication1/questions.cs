using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class questions
    {
        private int id;
        private int[] arrangement;
        public void setId(int i)
        {
            id = i;
        }
        public int getId()
        {
            return id;
        }
        public void setArr(int[] i)
        {
            arrangement = i;
        }
        public int[] getArr()
        {
            return arrangement;
        }
    }
}