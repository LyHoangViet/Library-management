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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuanLyKTX
{
    public partial class Phong : Form
    {
        public string connectionString = "Data Source=HOANGVIET\\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        public Phong()
        {
            InitializeComponent();

            cbTrangThai.Items.Add("On");
            cbTrangThai.Items.Add("Off");
        }
        private void TimKiemSinhVienTheoMNV(string mssv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "SELECT * FROM QLPhong WHERE MaPhong = @MSSV";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MSSV", mssv);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Gán dữ liệu vào các button
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        cbSoPhong.Text = row["MaPhong"].ToString();
                        cbNhanVien.Text = row["MaNV"].ToString();
                        cbTrangThai.Text = row["TrangThai"].ToString();
                    }
                    else
                    {
                        // Nếu không tìm thấy sinh viên, đặt giá trị trống cho các button
                        cbSoPhong.Text = "";
                        tbSoLuong.Text = "";
                        cbNhanVien.Text = "";
                        cbTrangThai.Text = "";
                        MessageBox.Show("Không tìm thấy phòng có mã phòng = " + mssv);
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
            string mssv = cbSoPhong.Text;
            TimKiemSinhVienTheoMNV(mssv);
        }

        private void Phong_Load(object sender, EventArgs e)
        {
            this.Location = new Point(445, 240);

            dataGridView1.CellClick += dataGridView1_CellContentClick;

            LoadDataToComboBox();
            LoadDataToComboBox2();
            LoadDataGirdview1();
        }

        private void LoadDataGirdview1()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Truy vấn SQL để lấy dữ liệu từ bảng NhanVien
                string query = "SELECT * FROM QLPhong";

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
                    cbSoPhong.Items.Clear();

                    // Đọc dữ liệu từ SqlDataReader và thêm vào ComboBox
                    while (reader.Read())
                    {
                        cbSoPhong.Items.Add(reader["MaPhong"].ToString());
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
        private void LoadDataToComboBox2()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Truy vấn danh sách các số phòng từ bảng newSinhVien
                    string query = "SELECT DISTINCT MaNV FROM NhanVien";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Xóa dữ liệu cũ trong ComboBox (nếu có)
                    cbNhanVien.Items.Clear();

                    // Đọc dữ liệu từ SqlDataReader và thêm vào ComboBox
                    while (reader.Read())
                    {
                        cbNhanVien.Items.Add(reader["MaNV"].ToString());
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
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tbSoLuong_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbSoPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Lấy số phòng được chọn từ ComboBox
                    string selectedRoom = cbSoPhong.SelectedItem.ToString();

                    // Truy vấn để đếm tổng số sinh viên trong phòng
                    string query = "SELECT COUNT(*) FROM newSinhVien WHERE MaPhong = @MaPhong";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaPhong", selectedRoom);

                    // Thực hiện truy vấn và lấy kết quả
                    int count = (int)command.ExecuteScalar();

                    // Hiển thị kết quả trong TextBox
                    tbSoLuong.Text = count.ToString();

                    // Đóng kết nối
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void ClearAll()
        {
            tbSoLuong.Clear();
        }
        private bool XoaSinhVien(string mnv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "DELETE FROM QLPhong WHERE MaPhong = @MNV";

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
            string mnv = cbSoPhong.Text;

            // Kiểm tra xem người dùng đã nhập MSSV hay chưa
            if (string.IsNullOrEmpty(mnv))
            {
                MessageBox.Show("Vui lòng chọn phòng bất kỳ.");
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng không?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Thực hiện xóa thông tin sinh viên
                if (XoaSinhVien(mnv))
                {
                    MessageBox.Show("Đã xóa phòng = " + mnv);
                    ClearAll(); // Xóa tất cả trường dữ liệu sau khi xóa thành công
                    LoadDataGirdview1();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa thông tin phòng.");
                }
            }
        }
        private bool CapNhatThongTinPhong(string maphong, string soluong, string manv, string trangthai)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "UPDATE QLPhong SET SoLuongSV = @SoLuong, MaNV = @MaNV, TrangThai = @TrangThai WHERE MaPhong = @MaPhong";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@SoLuong", soluong);
                    cmd.Parameters.AddWithValue("@MaNV", manv);
                    cmd.Parameters.AddWithValue("@TrangThai", trangthai);
                    cmd.Parameters.AddWithValue("@MaPhong", maphong);

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
            string maphong = cbSoPhong.Text;
            string soluong = tbSoLuong.Text;
            string manv = cbNhanVien.Text;
            string trangthai = cbTrangThai.Text;

            // Thực hiện cập nhật thông tin nhân viên vào cơ sở dữ liệu
            if (CapNhatThongTinPhong(maphong, soluong, manv, trangthai))
            {
                MessageBox.Show("Đã cập nhật thông tin phòng.");
                ClearAll();
                LoadDataGirdview1();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin phòng.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị từ các ô trong hàng đã click
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string maPhong = selectedRow.Cells["MaPhong"].Value.ToString();
                string soluong = selectedRow.Cells["SoLuongSV"].Value.ToString();
                string manv = selectedRow.Cells["MaNV"].Value.ToString();
                string trangthai = selectedRow.Cells["TrangThai"].Value.ToString();


                // Gán giá trị vào các TextBox tương ứng
                cbSoPhong.Text = maPhong;
                tbSoLuong.Text = soluong;
                cbNhanVien.Text = manv;
                cbTrangThai.Text = trangthai;
            }
        }
    }
}
