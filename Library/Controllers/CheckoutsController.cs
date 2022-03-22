//     public async Task<ActionResult> Index()
// {
//     var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//     var currentUser = await _userManager.FindByIdAsync(userId);
//     var userBooks = _db.Books.Where(entry => entry.User.Id == currentUser.Id).ToList();
//     return View(userBooks);
// }