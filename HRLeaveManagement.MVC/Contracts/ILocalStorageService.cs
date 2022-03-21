using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Contracts
{
    //to store authentication using JSON token or JWT
    public interface ILocalStorageService
    {
        void ClearStorage(List<string> keys);
        bool Exists(string key);
        T GetStorageValue<T>(string key);
        void SetStorageValue<T>(string key, T value);
    }
}
