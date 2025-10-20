# ✅ CitizenReport — Final To-Do List (POE Part 3)

This checklist ensures every rubric category reaches **Greatly Exceeds Required Standard (100%)**.

---

## 🧩 1 | Advanced Data Structures (50 Marks)

### Trees (20 Marks)

* [x] Binary Search Tree implemented for issue sorting.
* [ ] Add AVL Tree or Red-Black Tree example (even simplified).
* [ ] Include efficiency comments (O(log n) insert/search) in code and README.
* [ ] Explain the role of tree traversal in organising service-request display.

### Heaps & Graphs (30 Marks)

* [ ] Implement `Graph.cs` — connect service categories (Water → Sanitation → Waste → Electricity).
* [ ] Implement `MinHeap.cs` — prioritise requests by urgency (Pending = 1, In Progress = 2, Resolved = 3).
* [ ] Demonstrate BFS/DFS traversal in the controller (console/log output acceptable).
* [ ] Add explanation in README about how these structures optimise relationships and performance.

---

## 🗾 2 | Implementation Report (40 Marks)

### Readme File Quality (10 Marks)

* [ ] Expand README with:

  * Compile & run instructions (Visual Studio, CLI).
  * Screenshots of each page.
  * Table of data structures and their roles.
  * Sample test data (Issue examples).
  * Author, version, and date.
* [ ] Ensure formatting is clean and professional.

### Data Structure Explanation (10 Marks)

* [ ] Document in detail:

  * Why BST was chosen (ordered lookup).
  * How HashSet improves unique filtering.
  * How PriorityQueue optimises Local Events.
  * How Graph and Heap improve Service Request logic.
* [ ] Include short efficiency table (Dictionary vs BST vs Graph).

### Project Completion Report (20 Marks)

* [ ] Write 2–3 pages covering:

  * **Overview:** what changed from Part 1 to Part 3.
  * **Challenges & Solutions:** file uploads, UI consistency, data structures.
  * **Testing:** how each feature was verified.
  * **Screenshots** showing working implementation.

---

## 🧠 3 | Key Learnings (5 Marks)

* [ ] Add bullet list of skills gained:

  * MVC structure, Bootstrap integration, file uploads.
  * Data structure implementation in C#.
  * Debugging and state handling in web context.

---

## ⚙️ 4 | Technology Recommendations (10 Marks)

* [ ] Suggest at least two improvements:

  * LiteDB or SQLite for persistence.
  * ASP.NET API or Azure Blob for upload storage.
* [ ] Justify each based on scalability, performance, or user experience.

---

## 🔨 5 | Feedback Integration (5 Marks)

* [ ] Create `CHANGELOG.md` listing updates from Part 1 & 2 feedback:

  * Added consistent layout and responsive design.
  * Unified back buttons and form spacing.
  * Added feedback messages for empty tables.
* [ ] Add short “Feedback Implemented” section to README.

---

## 🎨 6 | Design & Responsiveness (10 Marks)

* [ ] Standardise spacing and container width across all views.
* [ ] Add responsive meta tag to `_Layout.cshtml`.
* [ ] Test with Chrome responsive mode (375 px to 1440 px).
* [ ] Ensure tables scroll horizontally on small screens.
* [ ] Confirm consistent font and colour scheme throughout.

---

## 🔖 7 | Academic Documentation & Referencing (5 Marks)

* [ ] Check Harvard accuracy:

  * Author (surname, initials). Year. *Title.* Publisher/URL. Accessed date.
* [ ] Add one academic source on data structures and one on MVC.

---

## 📂 8 | Submission Packaging

* [ ] Include in ZIP / GitHub:

  * `CitizenReport.sln` and all .cs files.
  * `README.md`
  * `ImplementationReport.md`
  * `ProjectCompletionReport.md`
  * `TechnologyRecommendations.md`
  * `CHANGELOG.md`
* [ ] Run clean build: `dotnet clean && dotnet build`
* [ ] Verify successful run before compressing.

---

### 📊 Final Mark Goal

| Category                | Current                      | Target |
| ----------------------- | ---------------------------- | ------ |
| Design & Responsiveness | 8/10                         | 10/10  |
| Data Structures         | 45/50 (pending Graph + Heap) | 50/50  |
| Documentation           | 8/10                         | 10/10  |
| Referencing & Feedback  | 4/5 + ?                      | 10/10  |
| **Projected Total**     | **95 ➔ 100 / 100**           | ✅      |
