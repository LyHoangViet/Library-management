using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuanLyKTX
{
    public partial class SinhVien : Form
    {
        string connectionString = "Data Source=HOANGVIET\\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        public SinhVien()
        {
            InitializeComponent();

            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");
        }

        private void formcon1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(445, 240);

            dataGridView1.CellClick += dataGridView1_CellContentClick;
            LoadDataToComboBox();
            LoadDataGirdview1();
        }
        private void LoadDataToComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Truy vấn danh sách các số phòng từ bảng newSinhVien
                    string query = "SELECT DISTINCT MaPhong FROM QLPhong";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Xóa dữ liệu cũ trong ComboBox (nếu có)
                    cbSoPhong1.Items.Clear();

                    // Đọc dữ liệu từ SqlDataReader và thêm vào ComboBox
                    while (reader.Read())
                    {
                        cbSoPhong1.Items.Add(reader["MaPhong"].ToString());
                    }

                    // Đóng kết nối
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void LoadDataGirdview1()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Truy vấn SQL để lấy dữ liệu từ bảng NhanVien
                string query = "SELECT * FROM newSinhVien";

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
        public void closeForm()
        {
            this.Hide();
        }

        
        private void tbDiaChi1_TextChanged(object sender, EventArgs e)
        {

        }
        private void ClearAll()
        {
            tbMSSV1.Clear();
            tbHoTen1.Clear();
            tbLop1.Clear();
            tbNgaySinh1.Clear();
            tbDiaChi1.Clear();
            tbSDT1.Clear();
            tbEmail1.Clear();
        }

        private void btThem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbMSSV1.Text != "" && tbHoTen1.Text != "" && tbLop1.Text != "" && tbNgaySinh1.Text != "" && cbGioiTinh.Text != "" && tbDiaChi1.Text != "" && tbSDT1.Text != "" && tbEmail1.Text != "")
                {
                    // Chuỗi kết nối đến cơ sở dữ liệu

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                        string query = "INSERT INTO newSinhVien (MaSV, MaPhong, HoTen, Lop, GioiTinh, NgaySinh, DiaChi, SoDienThoai, Email) " +
                                       "VALUES (@MSSV, @Phong, @Ten, @Lop, @GioiTinh, @NgaySinh, @DiaChi, @SoDienThoai, @Email)";

                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@MSSV", tbMSSV1.Text);
                        cmd.Parameters.AddWithValue("@Phong", cbSoPhong1.Text);
                        cmd.Parameters.AddWithValue("@Ten", tbHoTen1.Text);
                        cmd.Parameters.AddWithValue("@Lop", tbLop1.Text);
                        cmd.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.Text);
                        cmd.Parameters.AddWithValue("@NgaySinh", tbNgaySinh1.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", tbDiaChi1.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", tbSDT1.Text);
                        cmd.Parameters.AddWithValue("@Email", tbEmail1.Text);

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

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbSDT1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void TimKiemSinhVienTheoMSSV(string mssv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "SELECT * FROM newSinhVien WHERE MaSV = @MSSV";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MSSV", mssv);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Gán dữ liệu vào các button
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        tbHoTen1.Text = row["HoTen"].ToString();
                        tbLop1.Text = row["Lop"].ToString();
                        tbNgaySinh1.Text = row["NgaySinh"].ToString();
                        cbGioiTinh.Text = row["GioiTinh"].ToString();
                        cbSoPhong1.Text = row["MaPhong"].ToString();
                        tbDiaChi1.Text = row["DiaChi"].ToString();
                        tbSDT1.Text = row["SoDienThoai"].ToString();
                        tbEmail1.Text = row["Email"].ToString();
                    }
                    else
                    {
                        // Nếu không tìm thấy sinh viên, đặt giá trị trống cho các button
                        tbHoTen1.Text = "";
                        tbLop1.Text = "";
                        tbNgaySinh1.Text = "";
                        cbGioiTinh.Text = "";
                        cbSoPhong1.Text = "";
                        tbSDT1.Text = "";
                        tbDiaChi1.Text = "";
                        tbEmail1.Text = "";
                        MessageBox.Show("Không tìm thấy Nhân viên có MSSV = " + mssv);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string mssv = tbMSSV1.Text;
            TimKiemSinhVienTheoMSSV(mssv);
        }
        private bool CapNhatThongTinSinhVien(string masv, string hoTen, string lop, string gioiTinh, string ngaySinh, string maphong, string diaChi, string soDienThoai, string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "UPDATE newSinhVien SET HoTen = @HoTen, Lop = @Lop, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, MaPhong = @MaPhong, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email WHERE MaSV = @MaSV";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@Lop", lop);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@MaPhong", maphong);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@MaSV", masv);

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
            string maNV = tbMSSV1.Text;
            string hoTen = tbHoTen1.Text;
            string lop = tbLop1.Text;
            string gioiTinh = cbGioiTinh.Text;
            string ngaySinh = tbNgaySinh1.Text;
            string maphong = cbSoPhong1.Text;
            string diaChi = tbDiaChi1.Text;
            string soDienThoai = tbSDT1.Text;
            string email = tbEmail1.Text;

            // Thực hiện cập nhật thông tin nhân viên vào cơ sở dữ liệu
            if (CapNhatThongTinSinhVien(maNV, hoTen, lop, gioiTinh, ngaySinh, maphong, diaChi, soDienThoai, email))
            {
                MessageBox.Show("Đã cập nhật thông tin sinh viên.");
                ClearAll();
                LoadDataGirdview1();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin sinh viên.");
            }
        }
        private bool XoaSinhVien(string mssv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "DELETE FROM newSinhVien WHERE MaSV = @MSSV";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MSSV", mssv);

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
            string mssv = tbMSSV1.Text;

            // Kiểm tra xem người dùng đã nhập MSSV hay chưa
            if (string.IsNullOrEmpty(mssv))
            {
                MessageBox.Show("Vui lòng nhập MSNV để xóa thông tin nhân viên.");
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên có MSNV = " + mssv + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Thực hiện xóa thông tin sinh viên
                if (XoaSinhVien(mssv))
                {
                    MessageBox.Show("Đã xóa thông tin nhân viên có MSNV = " + mssv);
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
                string masv = selectedRow.Cells["MaSV"].Value.ToString();
                string hoten = selectedRow.Cells["HoTen"].Value.ToString();
                string lop = selectedRow.Cells["Lop"].Value.ToString();
                string ngaysinh = selectedRow.Cells["NgaySinh"].Value.ToString();
                string gioitinh = selectedRow.Cells["GioiTinh"].Value.ToString();
                string sophong = selectedRow.Cells["MaPhong"].Value.ToString();
                string diachi = selectedRow.Cells["DiaChi"].Value.ToString();
                string sdt = selectedRow.Cells["SoDienThoai"].Value.ToString();
                string email = selectedRow.Cells["Email"].Value.ToString();


                // Gán giá trị vào các TextBox tương ứng
                tbMSSV1.Text = masv;
                tbHoTen1.Text = hoten;
                tbLop1.Text = lop;
                tbNgaySinh1.Text = ngaysinh;
                cbGioiTinh.Text = gioitinh;
                cbSoPhong1.Text = sophong;
                tbDiaChi1.Text = diachi;
                tbSDT1.Text = sdt;
                tbEmail1.Text = email;
            }
        }
    }
}
