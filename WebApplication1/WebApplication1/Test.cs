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
        private List<string> answers = new List<string>();
        private List<string> youranwsers = new List<string>();
        private List<int> answerid = new List<int>();
        private List<int> aid = new List<int>();
        private List<string> correct = new List<string>();

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
            answers.Add(a);
        }
        public List<string> getAnswers()
        {
            return answers;
        }
        public void setYouranwser(string s)
        {
            youranwsers.Add(s);
        }
        public List<string> getYouranswers()
        {
            return youranwsers;
        }
        public void setAid(int i)
        {
            answerid.Add(i);
        }
        public List<int> getAid()
        {
            return answerid;
        }
        public void setAnsId(int i)
        {
            aid.Add(i);
        }
        public List<int> getAnsId()
        {
            return aid;
        }
        public void setTor(string s)
        {
            correct.Add(s);
        }
        public List<string> getTor()
        {
            return correct;
        }
    }

}