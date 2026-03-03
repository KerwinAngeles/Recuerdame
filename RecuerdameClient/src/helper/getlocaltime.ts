const getLocalDateTime = () => {
    const now = new Date()
    const offset = now.getTimezoneOffset() * 60000
    const localISO = new Date(now.getTime() - offset)
        .toISOString()
        .slice(0, 16)

    return localISO
}

export default getLocalDateTime