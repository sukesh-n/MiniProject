var mainCategory = document.getElementById('mainCategory');
var subCategory = document.getElementById('subCategory');

allCategories = [];
document.addEventListener('DOMContentLoaded', function() { 
    console.log('mainCategory changed');

    fetch(`http://localhost:5246/api/Common/GetAllCategories`, {
        method: "GET",
        headers: {
            "content-type": "application/json",
        }
    })
    .then(response => {
        if (response.ok) {
            return response.json();
        } else {
            throw new Error('Failed to get categories');
        }
    })
    .then(data => {
        console.log('Categories fetched successfully:', data);
        mainCategory.innerHTML = '<option value="">Select Main Category</option>'; // Clear previous options
        allCategories = data;
        data.forEach(category => {
            const option = document.createElement('option');
            option.value = category.mainCategory; 
            option.textContent = category.mainCategory; 
            mainCategory.appendChild(option); 
        });

        subCategory.style.display = "block"; // Show subCategory select after populating options
    })
    .catch(error => {
        console.error('Error fetching categories:', error);
        
    });
});

function loadSubCategory(mainCategory){
    subCategory.style.display = "block";
    console.log(mainCategory);
    subCategory.innerHTML = '<option value="">Select Sub Category</option>';     
    allCategories.forEach(sub => {
        const option = document.createElement('option');
        if(sub.mainCategory === mainCategory){
            option.value = sub.subCategoryId; 
            option.textContent = sub.subCategory; 
            subCategory.appendChild(option); 
            subCategory.appendChild(option); 
        }
        
    });
}