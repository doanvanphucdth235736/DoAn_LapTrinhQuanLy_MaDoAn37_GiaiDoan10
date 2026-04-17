using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using QuanLyPhongTroTheoThang.Data;

namespace QuanLyPhongTroTheoThang.Forms
{
    public partial class frmPrintPreview : Form
    {
        public ReportViewer reportViewer1;
        public frmPrintPreview()
        {
            InitializeComponent();

            reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill; // Cho nó tràn viền
            this.Controls.Add(reportViewer1);
        }

        private void frmPrintPreview_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        public void LoadHoaDon(int billId)
        {
            using (var db = new QLPTDbContext())
            {
                var bill = db.Bills
                    .Include(b => b.Contract).ThenInclude(c => c.Room)
                    .Include(b => b.Contract).ThenInclude(c => c.Tenant)
                    .FirstOrDefault(b => b.BillID == billId);

                if (bill == null) return;

                DataTable dt = new DataTable();
                dt.Columns.Add("RoomName");
                dt.Columns.Add("TenantName");
                dt.Columns.Add("Thang");
                dt.Columns.Add("TienPhong"); // Kiểm tra xem trên RDLC ô này có ghi là [TienPhong] không
                dt.Columns.Add("ChiSoDien");
                dt.Columns.Add("TongTienDien");
                dt.Columns.Add("ChiSoNuoc"); // Thêm cột nước
                dt.Columns.Add("TongTienNuoc"); // Thêm cột tiền nước
                dt.Columns.Add("TongCong");
                dt.Columns.Add("GhiChu"); // Thêm cột ghi chú
                dt.Columns.Add("NguoiLap");

                // Đổ dữ liệu thật cẩn thận
                dt.Rows.Add(
                    bill.Contract.Room.RoomName,
                    bill.Contract.Tenant.TenantName,
                    bill.Month.ToString("MM/yyyy"),
                    bill.RoomPrice.ToString("N0"), // Lấy giá phòng từ database
                    $"Mới: {bill.ElectricNew} - Cũ: {bill.ElectricOld}",
                    ((bill.ElectricNew - bill.ElectricOld) * 3500).ToString("N0"),
                    $"Mới: {bill.WaterNew} - Cũ: {bill.WaterOld}", // Chỉ số nước
                    ((bill.WaterNew - bill.WaterOld) * 15000).ToString("N0"), // Tiền nước (giả sử 15k)
                    bill.Total.ToString("N0"),
                    bill.Notes ?? "", // Nhét ghi chú vào đây
                    bill.CreatedBy
                                );

                
                this.reportViewer1.LocalReport.ReportPath = @"Reports\rptHoaDon.rdlc";

                this.reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                this.reportViewer1.LocalReport.DataSources.Add(rds);

                this.reportViewer1.RefreshReport();
            }
        }
        public void LoadThongKeHoaDon(List<Bill> danhSach)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RoomName");
            dt.Columns.Add("TenantName");
            dt.Columns.Add("Month");
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("Status");

            foreach (var item in danhSach)
            {
                dt.Rows.Add(
                    item.Contract.Room.RoomName,
                    item.Contract.Tenant.TenantName,
                    item.Month.ToString("MM/yyyy"),
                    item.Total,
                    item.Status ? "Đã thanh toán" : "Chưa thanh toán"
                );
            }

            this.reportViewer1.LocalReport.ReportPath = @"Reports\rptThongKeHoaDon.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            this.reportViewer1.LocalReport.DataSources.Add(rds);

            this.reportViewer1.RefreshReport();
        }

        public void LoadThongKeHopDong(List<Contract> danhSach)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ContractID");
            dt.Columns.Add("RoomName");
            dt.Columns.Add("TenantName");
            dt.Columns.Add("StartDate");
            dt.Columns.Add("EndDate");
            dt.Columns.Add("Deposit");
            dt.Columns.Add("Status");

            foreach (var c in danhSach)
            {
                dt.Rows.Add(
                    c.ContractID,
                    c.Room?.RoomName ?? "N/A",
                    c.Tenant?.TenantName ?? "N/A",
                    c.StartDate.ToString("dd/MM/yyyy"),
                    c.EndDate.ToString("dd/MM/yyyy"),
                    c.Deposit.ToString("N0"),
                    c.ContractStatus
                );
            }

            this.reportViewer1.LocalReport.ReportPath = @"Reports\rptThongKeHopDong.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("ThongKe", dt);
            this.reportViewer1.LocalReport.DataSources.Add(rds);

            this.reportViewer1.RefreshReport();
        }
    }
}
