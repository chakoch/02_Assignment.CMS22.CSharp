using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Assignment.CMS22.WPF.Services
{
    internal interface IFileManager // interface (kontrakt)
    {
        public void Save(string filePath, string content);
        public string Read(string filePath);
    }
    internal class FileManager : IFileManager // Nedan är methods på hur man sparar och läser listan
    {
        public string Read(string filePath)
        {
            using var sr = new StreamReader(filePath);
            return sr.ReadToEnd();
        }

        public void Save(string filePath, string content)
        {
            using var sw = new StreamWriter(filePath);
            sw.WriteLine(content);
        }
    }
}
