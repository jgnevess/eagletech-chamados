import React from "react";

export interface PropsAlert {
    type: 'alert alert-primary' | 'alert alert-success' | 'alert alert-danger'
    message: string
}

const Alert = (props: PropsAlert) => (
    <div style={{ zIndex: 999 }} className={props.type + ' w-25 position-absolute top-0 start-50 translate-middle-x mt-5'} role="alert">
        {props.message}
    </div>
)

export default Alert;