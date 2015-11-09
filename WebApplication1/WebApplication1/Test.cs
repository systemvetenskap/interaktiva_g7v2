using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Test
    {
        private string id;
        private string question;
        private string[] answers;
        private string[] youranwsers;
        private int i = 0;
        private int y = 0;
        public void setId(string i)
        {
            id = i;
        }
        public string getId()
        {
            return id;
        }
        public void setQuestion(string q)
        {
            question = q;
        }
        public string getQuestion()
        {
            return question;
        }
        public void setAnswers(string a)
        {
            int add = 1;
            answers[i] = a;
            i += add;
        }
        public string[] getAnswers()
        {
            return answers;
        }
        public void setYouranwser(string s)
        {
            youranwsers[y] = s;
            y += 1;
        }
        public string[] getYouranswers()
        {
            return youranwsers;
        }
    }

}