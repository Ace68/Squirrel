using System;

namespace Squirrel.Services.Abstracts
{
    public class DialogCommand
    {
        public object Id { get; set; }
        public string Label { get; set; }
        public Action Invoked { get; set; }
    }
}
