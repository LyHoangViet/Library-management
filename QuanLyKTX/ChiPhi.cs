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
    public partial class ChiPhi : Form
    {
        string connectionString = "Data Source=HOANGVIET\\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        public ChiPhi()
        {
            InitializeComponent();

        }

        private void ChiPhi_Load(object sender, EventArgs e)
        {
            this.Location = new Point(445, 240);

            dataGridView1.CellClick += dataGridView1_CellContentClick;
            LoadChiPhiData();
            LoadDataToComboBox();
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
        private void btThanhTien_Click(object sender, EventArgs e)
        {

            /*Tiền Nước
              Bậc 1: 0 - 50kwh là 1500đ
              Bậc 2: 50 - 100kwh là 1700đ
              Bậc 3: 100 - 200kwh là 2000đ
              Bậc 4: 200kwm - ... là 2500đ*/
            /*Tiền Điện
              Bậc 1: 0 - 10m3 là 6000đ
              Bậc 2: 10 - 20m3 là 7000đ
              Bậc 3: 20 - 30m3 là 9000đ
              Bậc 4: 30m3 - ... là 15000đ*/
            if (double.TryParse(tbTienDien.Text, out double kwh) && double.TryParse(tbTienNuoc.Text, out double m3))
            {
                double tienDien = 0;
                double tienNuoc = 0;

                // Tính tiền điện
                if (kwh <= 50)
                {
                    tienDien = kwh * 1500;
                }
                else if (kwh <= 100)
                {
                    tienDien = 50 * 1500 + (kwh - 50) * 1700;
                }
                else if (kwh <= 200)
                {
                    tienDien = 50 * 1500 + 50 * 1700 + (kwh - 100) * 2000;
                }
                else
                {
                    tienDien = 50 * 1500 + 50 * 1700 + 100 * 2000 + (kwh - 200) * 2500;
                }

                // Tính tiền nước
                if (m3 <= 10)
                {
                    tienNuoc = m3 * 6000;
                }
                else if (m3 <= 20)
                {
                    tienNuoc = 10 * 6000 + (m3 - 10) * 7000;
                }
                else if (m3 <= 30)
                {
                    tienNuoc = 10 * 6000 + 10 * 7000 + (m3 - 20) * 9000;
                }
                else
                {
                    tienNuoc = 10 * 6000 + 10 * 7000 + 10 * 9000 + (m3 - 30) * 15000;
                }

                // Tính tổng tiền
                double tongTien = tienDien + tienNuoc;

                // Hiển thị tổng tiền trong TextBox kết quả
                // Format số tiền để có dấu phẩy ngăn cách hàng nghìn
                tbTongTien.Text = tongTien.ToString("N0");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số lượng kWh và m3 hợp lệ.");
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Đảm bảo rằng DataGridView đã được tải với dữ liệu ChiPhi khi form được khởi động.
            LoadChiPhiData();
        }
        private void ClearAll()
        {
            tbTienDien.Clear();
            tbTienNuoc.Clear();
            tbTongTien.Clear();
            tbTime.Clear();
        }
        private void LoadChiPhiData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo một câu truy vấn SQL để lấy dữ liệu từ bảng ChiPhi
                string query = "SELECT * FROM ChiPhi";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // Tạo một DataTable để lưu trữ dữ liệu từ SQL Server
                        DataTable chiPhiTable = new DataTable();

                        // Đổ dữ liệu từ câu truy vấn vào DataTable
                        adapter.Fill(chiPhiTable);

                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        dataGridView1.DataSource = chiPhiTable;
                    }
                }
            }
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các control (ví dụ: TextBox) trong form
            int maPhong = int.Parse(cbSoPhong.Text); // Thay thế txtMaPhong bằng TextBox để nhập MaPhong
            int tienDien = int.Parse(tbTienDien.Text); // Thay thế txtTienDien bằng TextBox để nhập TienDien
            int tienNuoc = int.Parse(tbTienNuoc.Text); // Thay thế txtTienNuoc bằng TextBox để nhập TienNuoc
            double tinhTong = double.Parse(tbTongTien.Text); // Tính tổng
            string thang = tbTime.Text;

            // Kết nối và thêm dữ liệu vào SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO ChiPhi (MaPhong, TienDien, TienNuoc, TinhTong, Thang) " +
                               "VALUES (@MaPhong, @TienDien, @TienNuoc, @TinhTong, @Thang)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong);
                    command.Parameters.AddWithValue("@TienDien", tienDien);
                    command.Parameters.AddWithValue("@TienNuoc", tienNuoc);
                    command.Parameters.AddWithValue("@TinhTong", tinhTong);
                    command.Parameters.AddWithValue("@Thang", thang);

                    // Thực hiện thêm dữ liệu
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Nếu thêm thành công, cập nhật DataGridView với dữ liệu mới
                        LoadChiPhiData();
                        MessageBox.Show("Thêm dữ liệu thành công!");
                        ClearAll();
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm dữ liệu!");
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị từ các ô trong hàng đã click
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string maPhong = selectedRow.Cells["MaPhong"].Value.ToString();
                string tienDien = selectedRow.Cells["TienDien"].Value.ToString();
                string tienNuoc = selectedRow.Cells["TienNuoc"].Value.ToString();
                string tong = selectedRow.Cells["TinhTong"].Value.ToString();
                string time = selectedRow.Cells["Thang"].Value.ToString();


                // Gán giá trị vào các TextBox tương ứng
                cbSoPhong.Text = maPhong;
                tbTienDien.Text = tienDien;
                tbTienNuoc.Text = tienNuoc;
                tbTongTien.Text = tong;
                tbTime.Text = time;
            }
        }
        private bool XoaSinhVien(string mnv, string time)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "DELETE FROM ChiPhi WHERE MaPhong = @MNV AND Thang = @thang";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MNV", mnv);
                    cmd.Parameters.AddWithValue("@thang", time);

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

        // ... Các phần còn lại của form và các sự kiện khác
        private void btXoa_Click(object sender, EventArgs e)
        {
            string mnv = cbSoPhong.Text;
            string time = tbTime.Text;

            // Kiểm tra xem người dùng đã nhập MSSV hay chưa
            if (string.IsNullOrEmpty(mnv))
            {
                MessageBox.Show("Vui lòng nhập số phòng và tháng để xóa chi phí phòng.");
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Thực hiện xóa thông tin sinh viên
                if (XoaSinhVien(mnv, time))
                {
                    MessageBox.Show("Đã xóa chi phí của phòng = " + mnv);
                    ClearAll(); // Xóa tất cả trường dữ liệu sau khi xóa thành công
                    LoadChiPhiData();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa thông tin phòng.");
                }
            }
        }

        private void dataTime_ValueChanged(object sender, EventArgs e)
        {

        }
        private void TimKiemSinhVien(string keyword)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "SELECT * FROM ChiPhi WHERE MaPhong LIKE @Keyword";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%"); // Tìm kiếm một phần của từ khóa

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Gán dữ liệu DataTable vào DataGridView
                    dataGridView1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void btTimKiem2_Click(object sender, EventArgs e)
        {
            string keyword = cbSoPhong.Text;
            TimKiemSinhVien(keyword);
        }
    }
}
