import Card from "antd/es/card/Card"
import { CardTitle } from "./CardTitle"

interface Props{
    books: Book[]
}
export const Books = ({books} : Props) => {
    return (
        <div className="cards">
            {books.map((book : Book) => (
                <Card key={book.id} title={<CardTitle title={book.title} price={book.price}></CardTitle>}></Card>
            ))}
        </div>
    )
}