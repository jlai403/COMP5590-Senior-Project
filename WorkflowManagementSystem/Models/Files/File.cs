using System;
using System.IO;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Files
{
    public class File : IEntity
    {
        public int Id { get; set; }
        public User User { get; set; }

        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }

        public void Update(User user, FileInputViewModel fileInputViewModel)
        {
            User = user;
            FileName = fileInputViewModel.FileName;
            Content = GetBytes(fileInputViewModel.Content);
            ContentType = fileInputViewModel.ContentType;
            FileId = Guid.NewGuid();
        }

        private byte[] GetBytes(Stream stream)
        {
            byte[] byteArray;
            using(var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                byteArray = ms.ToArray();
            }
            return byteArray;
        }
    }
}
