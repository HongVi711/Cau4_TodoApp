const API_BASE = "http://localhost:5026/api/tasks";

export const getAllTasks = async () => {
  const res = await fetch(API_BASE);
  if (!res.ok) throw new Error("Failed to fetch tasks");
  return res.json();
};

export const createTask = async ({ title, description }) => {
  const res = await fetch(API_BASE, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ title, description }),
  });
  if (!res.ok) throw new Error("Failed to create task");
  return res.json();
};

export const updateTask = async (id, title, description,isCompleted ) => {
  const res = await fetch(`${API_BASE}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ id, title, description, isCompleted }),
  });
  if (!res.ok) throw new Error("Failed to update task");
  return res.json();
};

export const deleteTask = async (id) => {
  await fetch(`${API_BASE}/${id}`, {
    method: "DELETE",
  });
};

export const toggleTask = async (id) => {
  await fetch(`${API_BASE}/${id}/toggle`, {
    method: "PATCH",
  });
};