using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Manager.Domain.Entities
{
    public class SubTask
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public string Description { get; set; }
        public Priority priority { get; set; }
        public Status status { get; set; }
        public Category category { get; set; }
        public List<Comment> Comments { get; set; }
        public SubTask() { Comments = []; }
        public SubTask(string title, string description, Priority prior, Status stat, Category categ)
        {
            Title = title;
            Description = description;
            priority = prior;
            status = stat;
            category = categ;
            CreateTime = DateTime.Now;
            Comments = [];
        }
        public void SetPriority(Priority prior)
        {
            priority = prior;
        }
        public void SetStatus(Status stat)
        {
            status = stat;
        }
        public void SetCategory(Category categ)
        {
            category = categ;
        }
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }
        public void EditComment(Comment oldComment, Comment newComment)
        {
            int index = Comments.IndexOf(oldComment);
            if (index != -1)
            {
                Comments[index] = newComment;
            }
        }
        public void RemoveComment(Comment comment)
        {
            Comments.Remove(comment);
        }
    }
}
