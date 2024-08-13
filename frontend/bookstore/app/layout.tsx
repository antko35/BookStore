import { Flex, Layout, Menu } from "antd";
import "./globals.css";
import Link from "next/link";
import { Content, Footer } from "antd/es/layout/layout";
import { text } from "stream/consumers";

const items = [
  {key : "home", label: <Link href={"/"}>Home</Link>},
  {key : "books", label: <Link href={"/books"}>Books</Link>},
]

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Layout style={{minHeight: "100vh"}}>
          <header>
            <Menu 
              theme="dark" 
              mode="horizontal" 
              items={items} 
              style={{flex: 1, minWidth: 0}}
            />
          </header>
          <Content style={{padding : "0 48px"}}>{children}</Content>
          <Footer stule={{textAlign : "center"}}>
            BookStore 
          </Footer>
        </Layout>
       
      </body>
    </html>
  );
}
