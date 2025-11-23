import React from 'react';
import './index.css';

interface Props {
    value: any
    set?: (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void
    onChange?: () => void;
    id: string
    type: 'text' | 'password' | 'email'
    placeholder: string
    label?: string
    inputStyle: 'input' | 'textarea'
    disabled?: boolean
}

const InputForm = ({ value, set, id, type, placeholder, label, inputStyle, disabled }: Props) => {
    const InputElement = inputStyle === "input" ? (
        <input
            value={value}
            onChange={set}
            type={type}
            className="form-control"
            id={id}
            placeholder={placeholder}
            disabled={disabled}
        />
    ) : (
        <textarea
            style={{
                resize: 'none',
                height: '35vh'
            }}
            value={value}
            onChange={set}
            className="form-control"
            id={id}
            placeholder={placeholder}
        />
    );

    return (
        <div className="form-floating mb-3">
            {InputElement}
            <label htmlFor="nome">{label ? label : placeholder}</label>
        </div>
    )
}

export default InputForm;