using System;

namespace Br.Utils
{
    public interface ISyncManager
    {
        void Exec();

        void Add(Action action);

        void Close();
    }
}
