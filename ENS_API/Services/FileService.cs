﻿using ENS_API.Data;
using ExcelDataReader;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text;

namespace ENS_API.Services
{
    public class FileService
    {
        public List<Contact> GetContacts(Stream fileStream)
        {
            var contacts = new List<Contact>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var reader = ExcelReaderFactory.CreateReader(fileStream))
            {
                do
                {
                    bool isHeaderRow = true;

                    while (reader.Read())
                    {
                        if (isHeaderRow)
                        {
                            isHeaderRow = false;
                            continue;
                        }
                        if (reader.GetValue(0) == null && reader.GetValue(1) == null && reader.GetValue(2) == null)
                        {
                            break;
                        }
                        if (reader.FieldCount >= 3)
                        {
                            var contact = new Contact
                            {
                                Id = Guid.NewGuid(),
                                Email = reader.GetString(0) ?? string.Empty,
                                PhoneNumber = reader.GetValue(1).ToString() ?? string.Empty,
                                PreferredMethod = reader.GetValue(2).ToString() ?? string.Empty,
                            };
                            contacts.Add(contact);
                        }
                    }
                } while (reader.NextResult());
            }
            return contacts;
        }
    }
}
