using System;
using System.ComponentModel.DataAnnotations;

namespace CommandDLL
{
    public interface IDocumentBuilder<T>: IDocumentBuilder
    {
        void SetContent(T content);
    }

}

