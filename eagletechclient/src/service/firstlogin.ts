

const handleFirstLogin = () => {
    return JSON.parse(sessionStorage.getItem("first")!)
}

export { handleFirstLogin }