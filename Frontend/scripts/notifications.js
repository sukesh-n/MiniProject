function fetchNotifications(){
    const UserId = localStorage.getItem('id');
    const email = localStorage.getItem('email');
    const role = localStorage.getItem('role');


    fetch(`http://localhost:5246/api/notifications/${UserId}`,{
        method: "GET",
        headers: {
            "content-type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem('token')}`,

        }
        
    })
}