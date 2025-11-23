import React from "react";
import { JSX } from "react/jsx-runtime";
import Navbar from "../navbar";

interface Props {
    children: JSX.Element
}

const Container = (props: Props) => {
    return (
        <div className="row" style={{ height: '100vh', overflow: 'hidden' }}>
            <div className="col-3" style={{ height: '100vh', overflowY: 'auto' }}>
                <Navbar role={sessionStorage.getItem("role")!} />
            </div>
            <div className="col-9" style={{ height: '100vh', overflowY: 'auto' }}>
                <div className="w-100 d-flex flex-column align-items-center p-4">
                    {props.children}
                </div>
            </div>
        </div>
    )
}

export default Container