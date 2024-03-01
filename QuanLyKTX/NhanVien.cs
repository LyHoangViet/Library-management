using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKTX
{
    public partial class NhanVien : Form
    {
        string connectionString = "Data Source=HOANGVIET\\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        public NhanVien()
        {
            InitializeComponent();

            cbGioiTInh.Items.Add("Nam");
            cbGioiTInh.Items.Add("Nữ");
        }

        private void CoSoVatChat_Load(object sender, EventArgs e)
        {
            this.Location = new Point(445, 240);

            dataGridView1.CellClick += dataGridView1_CellContentClick;
            LoadDataGirdview1();
        }
        private void LoadDataGirdview1()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Truy vấn SQL để lấy dữ liệu từ bảng NhanVien
                string query = "SELECT * FROM NhanVien";

                // Tạo một SqlDataAdapter để lấy dữ liệu từ SQL Server
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                // Tạo một DataTable để lưu trữ dữ liệu
                DataTable dataTable = new DataTable();

                // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                adapter.Fill(dataTable);

                // Gán DataTable làm nguồn dữ liệu cho DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void TimKiemSinhVienTheoMNV(string mssv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "SELECT * FROM NhanVien WHERE MaNV = @MSSV";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MSSV", mssv);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Gán dữ liệu vào các button
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        tbHoTen.Text = row["HoTen"].ToString();
                        dtNgaySinh.Text = row["NgaySinh"].ToString();
                        cbGioiTInh.Text = row["GioTinh"].ToString();
                        tbDiaChi.Text = row["DiaChi"].ToString();
                        tbSdt.Text = row["SDT"].ToString();
                        tbChucVu.Text = row["ChucVu"].ToString();
                    }
                    else
                    {
                        // Nếu không tìm thấy sinh viên, đặt giá trị trống cho các button
                        tbHoTen.Text = "";
                        dtNgaySinh.Text = "";
                        cbGioiTInh.Text = "";
                        tbSdt.Text = "";
                        tbDiaChi.Text = "";
                        tbChucVu.Text = "";
                        MessageBox.Show("Không tìm thấy Nhân viên có MNV = " + mssv);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string mssv = tbMNV.Text;
            TimKiemSinhVienTheoMNV(mssv);
        }
        private void ClearAll()
        {
            tbMNV.Clear();
            tbHoTen.Clear();
            dtNgaySinh.Clear();
            tbSdt.Clear();
            tbDiaChi.Clear();
            tbChucVu.Clear();
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbMNV.Text != "" && tbHoTen.Text != "" && tbSdt.Text != "" && tbDiaChi.Text != "" && tbChucVu.Text != "")
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                        string query = "INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioTinh, DiaChi, SDT, ChucVu) " +
                                       "VALUES (@MNV, @Ten, @NgaySinh, @GioiTinh, @DiaChi, @SDT, @ChucVu)";

                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@MNV", tbMNV.Text);
                        cmd.Parameters.AddWithValue("@Ten", tbHoTen.Text);
                        cmd.Parameters.AddWithValue("@GioiTinh", cbGioiTInh.Text);
                        cmd.Parameters.AddWithValue("@NgaySinh", dtNgaySinh.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", tbDiaChi.Text);
                        cmd.Parameters.AddWithValue("@SDT", tbSdt.Text);
                        cmd.Parameters.AddWithValue("@ChucVu", tbChucVu.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Đăng ký thành công.");
                        ClearAll();
                        LoadDataGirdview1();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }
        private bool CapNhatThongTinNhanVien(string maNV, string hoTen, string gioiTinh, string ngaySinh, string diaChi, string soDienThoai, string chucVu)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "UPDATE NhanVien SET HoTen = @HoTen, GioTinh = @GioiTinh, NgaySinh = @NgaySinh, DiaChi = @DiaChi, SDT = @SoDienThoai, ChucVu = @ChucVu WHERE MaNV = @MaNV";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                    cmd.Parameters.AddWithValue("@MaNV", maNV);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được cập nhật hay không
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            // Lấy thông tin cần cập nhật từ các trường dữ liệu đã chỉnh sửa
            string maNV = tbMNV.Text;
            string hoTen = tbHoTen.Text;
            string gioiTinh = cbGioiTInh.Text;
            string ngaySinh = dtNgaySinh.Text;
            string diaChi = tbDiaChi.Text;
            string soDienThoai = tbSdt.Text;
            string chucVu = tbChucVu.Text;

            // Thực hiện cập nhật thông tin nhân viên vào cơ sở dữ liệu
            if (CapNhatThongTinNhanVien(maNV, hoTen, gioiTinh, ngaySinh, diaChi, soDienThoai, chucVu))
            {
                MessageBox.Show("Đã cập nhật thông tin nhân viên.");
                ClearAll();
                LoadDataGirdview1();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin nhân viên.");
            }
        }
        private bool XoaSinhVien(string mnv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "DELETE FROM NhanVien WHERE MaNV = @MNV";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MNV", mnv);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được xóa hay không
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }
        private void btXoa_Click(object sender, EventArgs e)
        {
            string mnv = tbMNV.Text;

            // Kiểm tra xem người dùng đã nhập MSSV hay chưa
            if (string.IsNullOrEmpty(mnv))
            {
                MessageBox.Show("Vui lòng nhập MSNV để xóa thông tin nhân viên.");
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên có MSNV = " + mnv + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Thực hiện xóa thông tin sinh viên
                if (XoaSinhVien(mnv))
                {
                    MessageBox.Show("Đã xóa thông tin nhân viên có MSNV = " + mnv);
                    ClearAll(); // Xóa tất cả trường dữ liệu sau khi xóa thành công
                    LoadDataGirdview1();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa thông tin Nhân viên.");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị từ các ô trong hàng đã click
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string manv = selectedRow.Cells["MaNV"].Value.ToString();
                string hoten = selectedRow.Cells["HoTen"].Value.ToString();
                string ngaysinh = selectedRow.Cells["NgaySinh"].Value.ToString();
                string gioitinh = selectedRow.Cells["GioTinh"].Value.ToString();
                string diachi = selectedRow.Cells["DiaChi"].Value.ToString();
                string sdt = selectedRow.Cells["SDT"].Value.ToString();
                string chucvu = selectedRow.Cells["ChucVu"].Value.ToString();


                // Gán giá trị vào các TextBox tương ứng
                tbMNV.Text = manv;
                tbHoTen.Text = hoten;
                dtNgaySinh.Text = ngaysinh;
                cbGioiTInh.Text = gioitinh;
                tbDiaChi.Text = diachi;
                tbSdt.Text = sdt;
                tbChucVu.Text = chucvu;
            }
        }
    }
}
