# 🧩 Wood Block Puzzle
## 🎮 Giới thiệu Game
**Wood Block Puzzle** là một tựa game xếp hình đơn giản.Nhiệm vụ của người chơi là kéo thả các khối gỗ có hình dạng khác nhau vào bảng chơi để lấp đầy các hàng ngang hoặc cột dọc nhằm ghi điểm. Mục đích là xem thử số điểm tối đa mình có thể đạt được

---

## 📥 Tải Game Chơi Thử
Bạn có thể tải bản build chơi thử mới nhất (dành cho Windows) tại mục **Releases**:

👉 **[Tải bản chơi thử tại đây (Releases)](https://github.com/nguyen009033-maker/wood_block_puzzle/releases)**

---

## 🛠 Cấu trúc Dự án

Dự án được tổ chức gọn gàng để dễ dàng quản lý và phát triển:

```text
wood_block_puzzle/
├── source/              # Toàn bộ Mã nguồn Unity (Assets, Scripts, Scenes, ProjectSettings)
├── .gitignore           # File cấu hình loại bỏ các file tạm/cache của Unity & VS Code
└── README.md            # Tài liệu giới thiệu dự án
```
## ⚙️ Nguyên lý Hoạt động & Kiến trúc 

### 1. Trạng thái Bàn chơi 
Bàn chơi được quản lý theo mảng dữ liệu (Grid Data) với 3 trạng thái cốt lõi đại diện cho từng ô:
* `0` - **Hide / Empty:** Ô trống, chưa có khối gỗ.
* `1` - **Hover:** Trạng thái xem trước (Preview) khi người chơi đang kéo khối gỗ đè lên vị trí hợp lệ.
* `2` - **Place / Occupied:** Ô đã được đặt khối gỗ cố định thành công.

### 2. Luồng xử lý Kéo thả & Duyệt bàn chơi 
* **Xác thực vị trí:** Khi thả khối gỗ, hệ thống sẽ đối chiếu vị trí con trỏ khối gỗ với mảng dữ liệu bàn chơi. Nếu tất cả các ô tương ứng đang ở trạng thái `0`, vị trí đó hợp lệ để đặt.
* **Cập nhật & Kiểm tra hàng/cột:** Ngay sau khi đặt block thành công (chuyển các ô tương ứng từ `0`/`1` $\rightarrow$ `2`), hệ thống sẽ **duyệt lại toàn bộ Bàn chơi (Board Scan)** theo cả hàng ngang và cột dọc để phát hiện các hàng/cột đã lấp đầy $100\%$ và kích hoạt cơ chế xóa ô + cộng điểm.

### 3. Âm thanh & Quản lý Dữ liệu 
* **Audio System:** Sử dụng **Audio Mixer** của Unity để phân luồng và điều chỉnh âm lượng tổng (Master), Nhạc nền (BGM) và Hiệu ứng âm thanh (SFX).
* **Data Persistence:** Sử dụng **PlayerPrefs** để lưu trữ điểm số cao nhất High Score và các cài đặt người dùng local.
* **Loading Screen:** Sử dụng cơ chế tải bất đồng bộ **`LoadSceneAsync`** để làm màn hình chờ chuyển Scene mượt mà, không bị giật khung hình chờ.
## 🎨 Tài nguyên Dự án 

* Dự án sử dụng kết hợp với các asset miễn phí  và hình ảnh được hỗ trợ tạo bởi trí tuệ nhân tạo(ChatGPT, Gemini):

