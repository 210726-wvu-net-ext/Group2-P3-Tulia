﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Database_Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }

        public Comment(int UserId, int PostId, string Content, DateTime Time)
        {
            this.UserId = UserId;
            this.PostId = PostId;
            this.Content = Content;
            this.Time = Time;
        }
    }
}
