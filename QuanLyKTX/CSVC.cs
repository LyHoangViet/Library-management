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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuanLyKTX
{
    public partial class CSVC : Form
    {
        string connectionString = "Data Source=HOANGVIET\\SQLEXPRESS;Initial Catalog=QLKTX;Integrated Security=True";
        public CSVC()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TimKiem_Load(object sender, EventArgs e)
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
        private void LoadChiPhiData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo một câu truy vấn SQL để lấy dữ liệu từ bảng ChiPhi
                string query = "SELECT * FROM CSVC";

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
        private void TimKiemSinhVien(string keyword)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "SELECT * FROM CSVC WHERE MaPhong LIKE @Keyword";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị từ các ô trong hàng đã click
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string maPhong = selectedRow.Cells["MaPhong"].Value.ToString();
                string csvc = selectedRow.Cells["TenCSVC"].Value.ToString();
                string soluong = selectedRow.Cells["SoLuong"].Value.ToString();
                string hientrang = selectedRow.Cells["HienTrang"].Value.ToString();


                // Gán giá trị vào các TextBox tương ứng
                cbSoPhong.Text = maPhong;
                tbCsvc.Text = csvc;
                tbSoLuong.Text = soluong;
                tbHientrang.Text = hientrang;
            }
        }
        private void ClearAll()
        {
            tbCsvc.Clear();
            tbHientrang.Clear();
            tbSoLuong.Clear();
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbCsvc.Text != "" && tbSoLuong.Text != "" && tbHientrang.Text != "")
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                        string query = "INSERT INTO CSVC (MaPhong, TenCSVC, SoLuong, HienTrang) " +
                                       "VALUES (@maphong, @Ten, @soluong, @hientrang)";

                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@maphong", cbSoPhong.Text);
                        cmd.Parameters.AddWithValue("@Ten", tbCsvc.Text);
                        cmd.Parameters.AddWithValue("@soluong", tbSoLuong.Text);
                        cmd.Parameters.AddWithValue("@hientrang", tbHientrang.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Thêm CSVC thành công.");
                        ClearAll();
                        LoadChiPhiData();
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
        private bool XoaSinhVien(string mnv, string tenCSVC)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng tham số trong truy vấn SQL để tránh SQL injection
                    string query = "DELETE FROM CSVC WHERE MaPhong = @MNV AND TenCSVC = @TenCSVC";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MNV", mnv);
                    cmd.Parameters.AddWithValue("@TenCSVC", tenCSVC);

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
        private void button2_Click(object sender, EventArgs e)
        {
            string mnv = cbSoPhong.Text;
            string tenCSVC = tbCsvc.Text;

            // Kiểm tra xem người dùng đã nhập MSSV hay chưa
            if (string.IsNullOrEmpty(mnv))
            {
                MessageBox.Show("Vui lòng nhập số phòng và tên CSVC bất kỳ để xóa.");
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Thực hiện xóa thông tin sinh viên
                if (XoaSinhVien(mnv, tenCSVC))
                {
                    MessageBox.Show("Đã xóa CSVC của phòng = " + mnv + " có tên " + tenCSVC);
                    ClearAll(); // Xóa tất cả trường dữ liệu sau khi xóa thành công
                    LoadChiPhiData();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa thông tin.");
                }
            }
        }
    }
}
