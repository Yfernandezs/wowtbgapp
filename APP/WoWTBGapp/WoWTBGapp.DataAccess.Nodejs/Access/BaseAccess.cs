using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWTBGapp.DataAccess.Abstractions;
using WoWTBGapp.DataObjects;
using Xamarin.Forms;

namespace WoWTBGapp.DataAccess.Nodejs
{
    public class BaseAccess<T> : IBaseAccess<T> where T : class, IBaseDataObject, new()
    {
        IAccessManager accessManager;

        public virtual string BaseAPIRouting => "";
        
        public BaseAccess()
        {

        }

        #region IBaseAccess implementation

        public async Task InitializeStore()
        {
            if (accessManager == null)
            {
                accessManager = DependencyService.Get<IAccessManager>();
            }

            if (!accessManager.IsInitialized)
            {
                await accessManager.InitializeAsync().ConfigureAwait(false);
            }
        }

        public virtual async Task<T> GetItemAsync(string id)
        {
            //throw new NotImplementedException();

            await InitializeStore().ConfigureAwait(false);

            var response = await accessManager.GetDataAsync(null, BaseAPIRouting).ConfigureAwait(false);

            if (response != null && response.IsSuccessful)
            {
                try
                {
                    // Deserializamos los datos formato JSON obtenidos por el servicio Web en una colección de empleados.
                    var deserializedData = JsonConvert.DeserializeObject<List<T>>(response.Data);

                    return deserializedData.Find(x => x._id == id);
                }
                catch (Exception ex) { }
            }

            return null;
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            //throw new NotImplementedException();

            await InitializeStore().ConfigureAwait(false);

            try
            {
                var response = await accessManager.GetDataAsync(null, BaseAPIRouting).ConfigureAwait(false);

                if (response != null && response.IsSuccessful)
                {
                    // Deserializamos los datos formato JSON obtenidos por el servicio Web en una colección de empleados.
                    var deserializedData = JsonConvert.DeserializeObject<List<T>>(response.Data);

                    return deserializedData;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return new List<T>();
        }

        public virtual async Task<bool> InsertAsync(T item)
        {
            //throw new NotImplementedException();

            return false;
        }

        public virtual async Task<bool> RemoveAsync(T item)
        {
            //throw new NotImplementedException();

            return false;
        }

        public virtual async Task<bool> SyncAsync()
        {
            //throw new NotImplementedException();

            return false;
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {
            //throw new NotImplementedException();

            return false;
        }

        #endregion IBaseAccess implementation

    }
}
