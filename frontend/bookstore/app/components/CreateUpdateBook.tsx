import React, { useEffect, useState } from 'react'
import { BookRequest } from '../Services/books';
import { Modal } from 'antd';
import { title } from 'process';
import TextArea from 'antd/es/input/TextArea';
import Input from 'antd/es/input/Input';

interface Props {
    mode: Mode;
    values: Book;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: BookRequest) => void;
    handleUpdate: (id: string, request: BookRequest) => void;
}

export enum Mode {
    Create,
    Edit
}
export const CreateUpdateBook = ({ mode, values, isModalOpen, handleCancel, handleCreate, handleUpdate }: Props) => {
    const[title,setTitle] = useState<string>("");
    const[description,setDescription] = useState<string>("");
    const[price,setPrice] = useState<number>(1);

    useEffect(() => {
        setTitle(values.title);
        setDescription(values.description);
        setPrice(values.price);
    },[values])

    const handleOnOk = async () => {
        const BookRequest = {title, description,price};
        
        mode == Mode.Create 
        ? handleCreate(BookRequest) 
        : handleUpdate(values.id,BookRequest);
    }


    return (
        <Modal 
            title={mode === Mode.Create ? "Добавить книгу" : "Редактировать"} 
            open={isModalOpen} 
            cancelText="Отмена"
            onOk={handleOnOk}
            onCancel={handleCancel}>
            <div className='book__modal'>
                <Input
                    value={title}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setTitle(e.target.value)}
                    placeholder='Название'
                />
                <TextArea 
                    value={description}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setDescription(e.target.value)}
                    autoSize={{minRows : 3, maxRows : 3}}
                    placeholder='Описание'
                />
                <Input
                    value={price}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setPrice(Number(e.target.value))}
                    placeholder='Цена'
                />
            </div>
        </Modal>
    )
}