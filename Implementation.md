# 🧾 CitizenReport — Implementation & Project Completion Report

This report provides a full summary of the **implementation process**, **technical challenges**, **project learnings**, and **technology recommendations** for the final MVC version of the **CitizenReport** application. It aligns with the POE Part 3 requirements for demonstrating practical data structure integration, problem-solving, and reflection.

---

## ⚙️ 1. Implementation Summary

The **CitizenReport MVC** application was designed to provide an efficient and responsive municipal service reporting system. Users can submit issues, track their service requests, and view local events. The application implements key **advanced data structures** (Binary Search Tree, Heap, Graph) to manage and optimise data.

### Core Functional Components

| Component                  | Description                                                              | Data Structures Used               |
| -------------------------- | ------------------------------------------------------------------------ | ---------------------------------- |
| **Report Issues**          | Collects issue details (category, description, location, attachments).   | Dictionary, HashSet                |
| **Local Events**           | Displays events in chronological order using sorted collections.         | SortedDictionary, PriorityQueue    |
| **Service Request Status** | Sorts and prioritises service issues; visualises category relationships. | Binary Search Tree, MinHeap, Graph |

### Data Storage

* Data is stored **in-memory** for demonstration purposes (no external database).
* Attachments are uploaded to `wwwroot/uploads/` during runtime.

---

## 🧠 2. Challenges & Solutions

| Challenge                                | Description                                                        | Solution                                                                                |
| ---------------------------------------- | ------------------------------------------------------------------ | --------------------------------------------------------------------------------------- |
| **Integrating multiple data structures** | Needed to meet rubric requirements while keeping MVC design clean. | Separated logic into utility classes (`BinarySearchTree.cs`, `Graph.cs`, `MinHeap.cs`). |
| **Ensuring responsive UI and feedback**  | Initial layout had inconsistencies and spacing issues.             | Standardised Bootstrap containers and unified color scheme.                             |
| **Data persistence for testing**         | The application used runtime-only storage.                         | Implemented static data stores to simulate persistence without a database.              |
| **File handling for uploads**            | Handling file validation and naming conflicts.                     | Implemented unique filenames via GUID and restricted accepted MIME types.               |

---

## 📘 3. Key Learnings & Insights

1. **Practical Understanding of Data Structures:** Learned how to integrate and visualise BSTs, heaps, and graphs in real-world MVC logic.
2. **Modular Design Thinking:** Separation of controller, model, and view logic improved maintainability and reusability.
3. **User Experience Emphasis:** Implemented responsive forms, success feedback, and cleaner UI components.
4. **Performance Awareness:** Improved lookup and sorting efficiency through structured algorithms.
5. **Problem-Solving Growth:** Applied algorithmic reasoning to achieve functional parity between academic and practical requirements.

---

## 🔧 4. Technology Recommendations

### 4.1 Short-Term Improvements

* **SQLite or LiteDB Integration:** Adds persistent local storage without heavy setup.
* **User Authentication:** Introduce login for residents and municipal staff using ASP.NET Identity.
* **Improved Logging:** Use `Serilog` for system event and error tracking.

### 4.2 Long-Term Enhancements

* **Cloud Deployment:** Host on **Azure App Service** or **Render** for real-world scalability.
* **API Expansion:** Implement a RESTful backend for external mobile clients.
* **Blob Storage:** Store uploaded files using **Azure Blob Storage** or **AWS S3** for durability and access control.

---

## ✅ 5. Conclusion

The final implementation of **CitizenReport (MVC)** successfully demonstrates:

* Integration of **advanced data structures** (BST, Heap, Graph) to enhance system logic.
* A fully functional, user-friendly municipal service reporting interface.
* Strong evidence of problem-solving, design thinking, and modern C# development practices.

