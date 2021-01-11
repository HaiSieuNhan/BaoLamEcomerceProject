using Ecommerce.Domain.Models;
using Ecommerce.Service.Interface;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Admin.ImportSuppliers
{
    public interface IImportSuppliersExcelService
    {
        Task<List<ImportSuppliersExcelData>> ImportFile(FileInfo file);
        Task<List<ImportSuppliersExcelData>> SaveImportFormFile(IFormFile file);
    }
    public class ImportSuppliersExcelService : IImportSuppliersExcelService
    {

        #region Services
        private readonly ISupplierService _supplierService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion

        public ImportSuppliersExcelService(IServiceProvider serviceProvider, IHostingEnvironment hostingEnvironment, ISupplierService supplierService)
        {
            _hostingEnvironment = hostingEnvironment;
            _serviceProvider = serviceProvider;
            _supplierService = supplierService;
        }

        public async Task<List<ImportSuppliersExcelData>> ImportFile(FileInfo file)
        {
            var dataExcell = await GetDataExcell(file);

            var supplierModels = await ValidateData(dataExcell);

            //CREATE FILE IF ERROR
            return supplierModels;
        }

        private async Task<List<ImportSuppliersExcelData>> ValidateData(List<ImportSuppliersExcelData> dataExcell)
        {
            var supplier = new List<ImportSuppliersExcelData>();
            foreach (var item in dataExcell)
            {
                var Supplier = new ImportSuppliersExcelData();

                if (string.IsNullOrEmpty(item.Name))
                {
                    Supplier.IsValid = false;
                    Supplier.Errors = " Không nhập fullName,";
                }
                Supplier.Name = item.Name;

                if (string.IsNullOrEmpty(item.CodeName))
                {
                    Supplier.Errors += " Không nhập mã người dùng,";
                    Supplier.CodeName = " Không nhập mã";
                }
                else
                {
                    Supplier.CodeName = item.CodeName;
                }

                if (string.IsNullOrEmpty(item.Email))
                {
                    Supplier.IsValid = false;
                    Supplier.Errors += " Không nhập Email,";
                }
                Supplier.Email = item.Email;

                if (string.IsNullOrEmpty(item.Phone))
                {
                    Supplier.IsValid = false;
                    Supplier.Errors += " Không nhập Số điện thoại,";
                }
                Supplier.Phone = item.Phone;

                Supplier.CreateDate = DateTime.Now;
                Supplier.Id = Guid.NewGuid();
                supplier.Add(Supplier);
            }
            return supplier;
        }

        private async Task<List<ImportSuppliersExcelData>> GetDataExcell(FileInfo file)
        {
            var importExcelDatas = new List<ImportSuppliersExcelData>();
            using (var package = new ExcelPackage(file))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets["user"];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                for (int row = 3; row <= rowCount; row++)
                {
                    importExcelDatas.Add(new ImportSuppliersExcelData
                    {
                       
                        Name = worksheet.Cells[row, 1].GetValue<string>()?.Trim(),
                        CodeName = worksheet.Cells[row, 2].GetValue<string>()?.Trim(),
                        Email = worksheet.Cells[row, 3].GetValue<string>()?.Trim(),
                        Phone = worksheet.Cells[row, 4].GetValue<string>()?.Trim(),
                        Fax = worksheet.Cells[row, 5].GetValue<string>()?.Trim(),
                        Sort = int.Parse(worksheet.Cells[row, 6].GetValue<string>()?.Trim()),
                    });
                }
            }

            return importExcelDatas;
        }

        public async Task<List<ImportSuppliersExcelData>> SaveImportFormFile(IFormFile file)
        {
            var filePath = Directory.GetCurrentDirectory() + "\\Excel\\";
            var fileName = $"Supplier-{DateTime.Now.Ticks}.xlsx";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath += fileName;
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            FileInfo fileInfo = new FileInfo(filePath);

            return await ImportFile(fileInfo);
        }
    }
    public class ImportSuppliersExcelData : Validate
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int Sort { get; set; }
    }
    public class Validate
    {
        public bool IsValid { get; set; } = true;
        public string Errors { get; set; }
        // Đánh dấu kết quả là ko hợp lệ
        //internal void MarkInvalid(string reason)
        //{
        //    IsValid = false;
        //    Errors.Add(reason);
        //}
    }
}
