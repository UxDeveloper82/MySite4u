
//Declare values
const membersList = document.getElementById('members');
const searchBar = document.getElementById('searchBar');
const pagination_element = document.getElementById('pagination');
let membersCharacters = [];
let current_page = 1;
let rows = 5;
// Search Or Filter Members
//searchBar.addEventListener('keyup', (e) => {
   // const searchString = e.target.value.toLowerCase();
    //const filteredMembers = membersCharacters.filter(members => {
        //return members.userName.toLowerCase().includes(searchString) ||
      //      members.knownAs.toLowerCase().includes(searchString);
    //});
  //  displayMembers(filteredMembers);
//}) 

// Display the Members
const loadMembers = async () => {
    try {
        const resp = await fetch('/api/members');
        membersCharacters = await resp.json();
        displayMembers(membersCharacters);
    } catch (err) {
        console.log(err);
    }
};
const displayMembers = (members) => {
    const htmlString = members.map((members) => {
                      return `
                              <div class="card card-box mb-4">
                                     <div class="card-img-wrapper-box">
                                           <img style="width:100%" class="img-thumbnail" src="/content/blog/${members.photo}" alt="${members.knownAs}" />
                                           <ul class="list-inline member-icons animate text-center">
                                               <li class="list-inline-item">
                                                   <a class="btn btn-primary btn-sm" href="/UI/Members/details/${members.id}">
                                                       <i class="fa fa-user"></i>
                                                   </a>
                                               </li>
                                               <li class="list-inline-item">
                                                   <a class="btn btn-primary btn-sm" target="_blank" href="https://www.facebook.com/Icreatesites4u-690772767726869">
                                                       <i class="fa fa-facebook"></i>
                                                   </a>
                                               </li>
                                               <li class="list-inline-item">
                                                   <a class="btn btn-primary btn-sm" target="_blank" href="https://github.com/UxDeveloper82">
                                                       <i class="fa fa-github"></i>
                                                   </a>
                                               </li>
                                           </ul>
                                    </div>
                                    <div class="card-body">
                                       <h6 class="card-title text-center mb-1">
                                           <i class="fa fa-user"></i>
                                                    KnownAs: ${members.knownAs},
                                                    LastActive: ${ new Date(members.lastActive).toLocaleDateString() }
                                       </h6>
                                       <p class="card-text text-muted text-center">From: ${members.city}</p>
                                       <p class="text-center text-uppercase">
                                            Dotnet Fullstack-developer
                                        </p>
                                   </div>
                             </div>`;
    }).join('');
    membersList.innerHTML = htmlString;
}
loadMembers();