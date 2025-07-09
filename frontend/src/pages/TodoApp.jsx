import React, { useState, useEffect } from "react"
import "./TodoApp.css"
import { Trash2, Edit, Save, Plus } from "lucide-react"
import {getAllTasks, createTask, updateTask, deleteTask as deleteTaskAPI, toggleTask } from "../api/tasksApi"

export default function TodoApp() {
    const [tasks, setTasks] = useState([])
    const [newTitle, setNewTitle] = useState("")
    const [newDescription, setNewDescription] = useState("")
    const [tab, setTab] = useState("all")

    useEffect(() => {
        loadTasks()
    }, [])

    const loadTasks = async () => {
        try {
        const data = await getAllTasks()
        const formatted = data.map((task) => ({ ...task, isEditing: false }))
        setTasks(formatted)
        } catch (err) {
        console.error("Lỗi khi tải task:", err)
        }
    }

    const addTask = async () => {
        if (!newTitle.trim()) return
        try {
        const newTask = await createTask({
            title: newTitle.trim(),
            description: newDescription.trim(),
        })
        setTasks([...tasks, { ...newTask, isEditing: false }])
        setNewTitle("")
        setNewDescription("")
        } catch (err) {
        console.error("Lỗi khi tạo task:", err)
        }
    }

    const deleteTask = async (id) => {
        try {
        await deleteTaskAPI(id)
        setTasks(tasks.filter((t) => t.id !== id))
        } catch (err) {
        console.error("Lỗi khi xóa:", err)
        }
    }

    const toggleComplete = async (id) => {
        try {
        await toggleTask(id)
        setTasks(
            tasks.map((task) =>
            task.id === id ? { ...task, isCompleted: !task.isCompleted } : task
            )
        )
        } catch (err) {
        console.error("Lỗi khi toggle:", err)
        }
    }

    const toggleEdit = (id) => {
        setTasks(
        tasks.map((task) =>
            task.id === id ? { ...task, isEditing: !task.isEditing } : task
        )
        )
    }

    const updateTitle = (id, value) => {
        setTasks(
        tasks.map((task) =>
            task.id === id ? { ...task, title: value } : task
        )
        )
    }

    const updateDescription = (id, value) => {
        setTasks(
        tasks.map((task) =>
            task.id === id ? { ...task, description: value } : task
        )
        )
    }

    const saveTask = async (id) => {
        const task = tasks.find((t) => t.id === id)
        if (!task) return
        try {
            await updateTask(id, task.title, task.description, task.isCompleted)
            setTasks(
            tasks.map((t) =>
                t.id === id ? { ...t, isEditing: false } : t
            )
            )
        } catch (err) {
            console.error("Lỗi khi cập nhật:", err)
        }
    }
    const formatDate = (date) => {
        return new Date(date).toLocaleString("vi-VN", {
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
        hour: "2-digit",
        minute: "2-digit",
        })
    }

      const filteredTasks = tasks.filter((task) => {
    if (tab === "all") return true
    if (tab === "incomplete") return !task.isCompleted
    if (tab === "complete") return task.isCompleted
    return true
  })

    return (
    <div className="todo-container">
        <h1 className="title">TODO APP</h1>

        {/* Tab chọn lọc */}
        <div className="tabs">
        <button
            className={tab === "all" ? "active" : ""}
            onClick={() => setTab("all")}
        >
            Tất cả
        </button>
        <button
            className={tab === "incomplete" ? "active" : ""}
            onClick={() => setTab("incomplete")}
        >
            Chưa hoàn thành
        </button>
        <button
            className={tab === "complete" ? "active" : ""}
            onClick={() => setTab("complete")}
        >
            Hoàn thành
        </button>
        </div>

        <div className="add-task">
        <input
            placeholder="Tiêu đề..."
            value={newTitle}
            onChange={(e) => setNewTitle(e.target.value)}
        />
        <input
            placeholder="Mô tả (tùy chọn)..."
            value={newDescription}
            onChange={(e) => setNewDescription(e.target.value)}
        />
        <button onClick={addTask}>
            <Plus size={16} /> Thêm
        </button>
        </div>

        {filteredTasks.length === 0 ? (
        <div className="no-task">Chưa có công việc nào</div>
        ) : (
        <ul className="task-list">
            {filteredTasks.map((task) => (
            <li
                key={task.id}
                className={`task-item ${task.isCompleted ? "completed" : ""}`}
            >
                <input
                type="checkbox"
                checked={task.isCompleted}
                onChange={() => toggleComplete(task.id)}
                />
                <div className="task-content">
                {task.isEditing ? (
                    <>
                    <input
                        value={task.title}
                        onChange={(e) => updateTitle(task.id, e.target.value)}
                        disabled={task.isCompleted} // disable nếu hoàn thành
                    />
                    <input
                        value={task.description || ""}
                        onChange={(e) => updateDescription(task.id, e.target.value)}
                        disabled={task.isCompleted} // disable nếu hoàn thành
                    />
                    </>
                ) : (
                    <>
                    <strong>{task.title}</strong>
                    {task.description && <p>{task.description}</p>}
                    <small>
                        Tạo: {formatDate(task.createdAt)}
                        {task.updatedAt && (
                        <> | Cập nhật: {formatDate(task.updatedAt)}</>
                        )}
                    </small>
                    </>
                )}
                </div>
                <div className="task-actions">
                {task.isEditing ? (
                    <button onClick={() => saveTask(task.id)}>
                    <Save size={16} />
                    </button>
                ) : (
                    // Ẩn nút edit nếu task đã hoàn thành
                    !task.isCompleted && (
                    <button onClick={() => toggleEdit(task.id)}>
                        <Edit size={16} />
                    </button>
                    )
                )}
                <button onClick={() => deleteTask(task.id)}>
                    <Trash2 size={16} />
                </button>
                </div>
            </li>
            ))}
        </ul>
        )}

        <div className="stats">
        Tổng: {tasks.length} | Hoàn thành: {tasks.filter((t) => t.isCompleted).length}
        </div>
    </div>
    )

}