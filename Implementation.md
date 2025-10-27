# CitizenReport — Implementation Report (POE Part 3)

This report provides a comprehensive explanation of the **implementation, configuration, and data structure integration** in the CitizenReport MVC application. It demonstrates how each structure contributes to the performance and efficiency of the *Service Request Status* feature, in accordance with POE Part 3 requirements.

---

## 1. System Overview

The **CitizenReport MVC** web application serves as a municipal reporting platform that allows residents to:

* Submit service requests with location and attachments.
* View and track issue progress.
* Access local events and announcements.

The focus of this phase is on implementing **advanced data structures** to improve **efficiency**, **organisation**, and **retrieval** of data within the application’s logic layer.

---

## 2. Data Structure Implementation and Contribution

### 2.1 Binary Search Tree (BST)

**Purpose:**
To efficiently store and retrieve service requests in an ordered structure based on their submission date or unique identifier.

**Implementation Details:**

* Each `Issue` object is inserted into a `BinarySearchTree<Issue>` using the `DateSubmitted` as the key.
* The `InOrderTraversal()` method ensures the requests are displayed in chronological order on the *Service Request Status* page.
* The traversal guarantees that the most recent issues are easily retrievable without additional sorting operations.

**Code Reference:**

```csharp
var tree = new BinarySearchTree<Issue>(requests);
var sorted = tree.InOrderTraversal();
```

**Contribution to Efficiency:**

* **Time Complexity:** Insertion/Search O(log n); Traversal O(n)
* **Effect:** Reduces redundant sorting and ensures that issue data remains organised dynamically as new reports are added.
* **Result:** Users experience faster loading and consistent chronological display when viewing the status of multiple service requests.

---

### 2.2 MinHeap (Priority Queue)

**Purpose:**
To manage issue priority levels efficiently, ensuring that urgent service requests (e.g., electricity faults or water leaks) are processed first.

**Implementation Details:**

* Issues are assigned a numeric priority (1 = High, 2 = Medium, 3 = Low).
* A MinHeap maintains the smallest (most urgent) value at the top of the structure.
* When issues are viewed or processed, the heap always surfaces the most critical ones first.

**Code Reference:**

```csharp
var heap = new PriorityQueue<Issue, int>();
heap.Enqueue(issue, issue.PriorityLevel);
```

**Contribution to Efficiency:**

* **Time Complexity:** Insertion/Deletion O(log n)
* **Effect:** Automatically maintains a sorted queue of pending issues by priority.
* **Result:** The interface can display high-priority reports immediately, simulating real-world municipal workflows.

---

### 2.3 Graph (Breadth-First Search)

**Purpose:**
To model and manage relationships between municipal service categories, ensuring logical traversal between interconnected departments.

**Implementation Details:**

* Nodes represent service categories such as *Water*, *Sanitation*, *Waste*, and *Electricity*.
* Edges connect related departments (e.g., Water → Sanitation → Waste).
* A Breadth-First Search (BFS) algorithm traverses connected nodes, demonstrating efficient category mapping.

**Code Reference:**

```csharp
var graph = new Graph<string>();
graph.AddEdge("Sanitation", "Water");
graph.AddEdge("Water", "Waste");
var traversal = graph.BFS("Sanitation");
```

**Contribution to Efficiency:**

* **Time Complexity:** O(V + E)
* **Effect:** Demonstrates understanding of graph traversal and its practical use in representing category interdependencies.
* **Result:** Ensures a flexible structure that can later support routing logic or inter-department escalation.

---

## 3. Integration Summary

Each data structure plays a role in creating a more organised, responsive, and efficient system:

| Structure              | Application Area                                             | Contribution                                                        |
| ---------------------- | ------------------------------------------------------------ | ------------------------------------------------------------------- |
| **Binary Search Tree** | Sorts and retrieves service requests in order of submission. | Eliminates redundant sorting; improves readability and performance. |
| **MinHeap**            | Prioritises urgent service requests.                         | Ensures high-priority issues are processed or displayed first.      |
| **Graph**              | Models relationships between service categories.             | Enables efficient traversal and logical linkage across departments. |

---

## 4. Compilation and Usage Summary

**To Compile:**

```bash
cd CitizenReportWeb
dotnet restore
dotnet build
```

**To Run:**

```bash
dotnet run
```

Then open the application in a browser (e.g., `https://localhost:7001`).

**Navigation:**

* *Home → Report Issues* to submit a new request.
* *Home → Service Request Status* to view all requests (ordered and prioritised using data structures).

---

## 5. Conclusion

The implementation of Binary Search Trees, MinHeaps, and Graphs demonstrates the practical application of theoretical data structures within a working ASP.NET MVC environment.
Each contributes to a distinct layer of the CitizenReport system — from **data organisation** and **prioritisation** to **relationship modelling** — ensuring that the *Service Request Status* feature performs efficiently and aligns with POE Part 3’s advanced structure requirements.
