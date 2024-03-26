using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Manager.Domain
{
    public class SubTask
    {
        public int SubTaskId { get; private set; }
        public string Title { get; private set; }
        public DateTime CreateTime { get; private set; }
        public string Description { get; private set; }
        public int PriorityId { get; private set; }
        public int StatusId { get; private set; }
        public int CategoryId { get; private set; }
        public List<Comment> Comments { get; private set; }

        public SubTask(string title, string description)
        {
            Title = title;
            Description = description;
            CreateTime = DateTime.Now;
            Comments = new List<Comment>();
        }
        public void SetPriority(int priorityId)
        {
            PriorityId = priorityId;
        }
        public void SetStatus(int statusId)
        {
            StatusId = statusId;
        }
        public void SetCategory(int categoryId)
        {
            CategoryId = categoryId;
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
