# 🧩 CitizenReport Data Structures Overview

This document explains how **Binary Search Trees**, **Heaps**, and **Graphs** are integrated into the CitizenReport MVC application to enhance data organisation, efficiency, and retrieval. These structures align with the POE Part 3 requirements for demonstrating advanced data structures in practical use.

---

## 🌳 Binary Search Tree (BST)

### Purpose

The Binary Search Tree is used to **organise and sort service requests** by their submission date or unique identifier. When a user views the *Service Request Status* page, the BST ensures that issues are displayed in an ordered manner.

### Implementation Summary

* Each `Issue` object is inserted into the tree based on its `DateSubmitted`.
* The `InOrderTraversal()` method retrieves issues in chronological order.

### Efficiency

* **Insertion / Search:** O(log n)
* **Traversal:** O(n)

### Practical Benefit

Efficiently manages and retrieves issue data, ensuring the display remains quick and logically ordered regardless of how many reports exist.

---

## ⚙️ MinHeap (Priority Queue)

### Purpose

The MinHeap structure is used to **prioritise service requests** based on urgency or category importance.

### Implementation Summary

* Each issue is assigned a numeric priority (e.g., 1 = high, 3 = low).
* The heap automatically keeps the most urgent requests accessible first.
* Used internally to demonstrate priority-based data management.

### Efficiency

* **Insertion:** O(log n)
* **Extract Minimum:** O(log n)

### Practical Benefit

Models a real-world municipal workflow where urgent requests (e.g., water leaks, power outages) are attended to first. This reinforces the system’s responsiveness and organisation logic.

---

## 🔗 Graph (Breadth-First Search)

### Purpose

The Graph structure represents **relationships between municipal service categories**, such as how one department or service type connects to others.

### Implementation Summary

* Nodes represent service categories (e.g., Water, Sanitation, Waste, Electricity).
* Edges show relationships between them.
* A Breadth-First Search (BFS) algorithm is used to traverse connected nodes.

### Efficiency

* **Traversal (BFS/DFS):** O(V + E)
  *(V = vertices, E = edges)*

### Practical Benefit

Demonstrates an understanding of traversal and relationship management, as required by the rubric. Useful for modelling dependencies between service types.

---

## 🧠 Summary Table

| Structure              | Purpose                | Example Use              | Time Complexity |
| ---------------------- | ---------------------- | ------------------------ | --------------- |
| **Binary Search Tree** | Sort & organise issues | Order by submission date | O(log n)        |
| **MinHeap**            | Manage priority        | Urgent reports first     | O(log n)        |
| **Graph (BFS)**        | Model category links   | Service relationships    | O(V + E)        |

---

### 📘 Conclusion

The CitizenReport application integrates **Tree**, **Heap**, and **Graph** structures at the data logic level to demonstrate mastery of algorithmic design in a real-world scenario. These structures ensure efficient management, prioritisation, and relationship mapping of community service requests while maintaining a clean and responsive user interface.
