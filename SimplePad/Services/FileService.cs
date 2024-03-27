using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace SimplePad.Services
{
    public class FileService
    {
        public IBuffer GetBufferFromLong(long lenght)
        {
            IBuffer buffer = new Windows.Storage.Streams.Buffer((uint)lenght);
            buffer.Length = (uint)lenght;

            return buffer;
        }

        public async Task SaveToStorageFile(StorageFile file, long text, long lenght)
        {
            if (file == null)
            {
                await FileIO.WriteBufferAsync(file, GetBufferFromLong(lenght));
            }
        }

        public async Task SaveToStorageFile(StorageFile file, long text, IBuffer buffer)
        {
            if (file == null)
            {
                await FileIO.WriteBufferAsync(file, buffer);
            }
        }

        public async Task<StorageFile> GetSaveFile()
        {
            FileSavePicker openPicker = new FileSavePicker();

            var window = ThemeService.m_window; //just pull it from there, not really ideal but it works
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            foreach (var item in LanguageDefinitions.GetAllDisplayNames())
            {
                List<string> ending = new List<string>();
                ending.Add(LanguageDefinitions.GetFileEndingFromDisplayName(item).Insert(0, "."));

                openPicker.FileTypeChoices.Add($"{item} File", ending);
            }

            // Open the picker for the user to pick a file
            var file = await openPicker.PickSaveFileAsync();

            return file;
        }
    }
}
