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
        private int i;
        private int y;
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
            
            answers[i] = a;
            i++;
        }
        public string[] getAnswers()
        {
            return answers;
        }
        public void setYouranwser(string s)
        {
            youranwsers[y] = s;
            y++;
        }
        public string[] getYouranswers()
        {
            return youranwsers;
        }
    }

}