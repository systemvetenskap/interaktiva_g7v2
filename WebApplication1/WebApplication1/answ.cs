using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class answ
    {
        private int id;
        private int count;
        private int answers;
        private bool correct;
        private string category; 

        public void setId(int i)
        {
            id = i;
        }
        public void setCount()
        {
            count++;
        }
        public int getId()
        {
            return id;
        }
        public int getCount()
        {
            return count;
        }
        public void setAnswer()
        {
            answers++;
        }
        public int getAnswers()
        {
            return answers;
        }
        public bool getCorrect()
        {
            if(count == answers)
            {
                correct = true;
            }
            else
            {
                correct = false;
            }
            return correct;
        } 
        public void setCat(string cat)
        {
            category = cat;
        }
        public string getCat()
        {
            return category;
        }
    }
}