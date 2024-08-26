import { headers } from "next/headers";

export interface BookRequest{
    title : string;
    description : string;
    price : number;
};

export const getCookie = (name: string): string | null => {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop()?.split(';').shift() || null;
    return null;
};

export const getAllBooks = async() => {
    const token = getCookie("cooky_try");
    const response = await fetch("http://localhost:5138/Books",{
        method: "GET",
        credentials: "include",
        headers:{
            "Authorization": `Bearer ${token}`,
            "content-type": "application/json",
        }
    });
    return response.json();
};

export const createBook = async(bookRequest : BookRequest) => {
    const token = localStorage.getItem("cooky_try");

    await fetch("http://localhost:5138/Books",{
        method: "POST",
        headers:{
            "Authorization": `Bearer ${token}`,
            "content-type": "application/json",
        },
        body: JSON.stringify(bookRequest),
    });
};

export const updateBook = async(id: string, bookRequest: BookRequest) =>{
    await fetch(`http://localhost:5138/Books/${id}`,{
        method: "PUT",
        headers:{
            "content-type": "application/json",
        },
        body: JSON.stringify(bookRequest),
    });
};

export const deleteBook = async(id : string) => {
    await fetch(`http://localhost:5138/Books/${id}`,{
        method: "DELETE"
    });
};