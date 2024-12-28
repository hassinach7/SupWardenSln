const characters = {
    lower: 'abcdefghijklmnopqrstuvwxyz',
    upper: 'ABCDEFGHIJKLMNOPQRSTUVWXYZ',
    numbers: '0123456789',
    special: '!@#$%^&*()_+-=[]{}|;:,.<>?'
};

const ambiguousCharacters = 'ILOS05';

function generatePassword(length, minUpper, minLower, minNumbers, minSpecial, avoidAmbiguous) {
    let password = '';
    const availableCharacters = {
        lower: avoidAmbiguous ? characters.lower.replace(/[ilo]/g, '') : characters.lower,
        upper: avoidAmbiguous ? characters.upper.replace(/[ILO]/g, '') : characters.upper,
        numbers: avoidAmbiguous ? characters.numbers.replace(/[05]/g, '') : characters.numbers,
        special: characters.special
    };

    // Add minimum required characters
    password += getRandomCharacters(availableCharacters.lower, minLower);
    password += getRandomCharacters(availableCharacters.upper, minUpper);
    password += getRandomCharacters(availableCharacters.numbers, minNumbers);
    password += getRandomCharacters(availableCharacters.special, minSpecial);

    // Fill the rest of the password length with random characters
    const allCharacters = availableCharacters.lower + availableCharacters.upper + availableCharacters.numbers + availableCharacters.special;
    for (let i = password.length; i < length; i++) {
        password += allCharacters[Math.floor(Math.random() * allCharacters.length)];
    }

    // Shuffle the password
    password = password.split('').sort(() => 0.5 - Math.random()).join('');
    return password;
}

function getRandomCharacters(characters, count) {
    let result = '';
    for (let i = 0; i < count; i++) {
        result += characters[Math.floor(Math.random() * characters.length)];
    }
    return result;
}

document.getElementById('passwordForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const length = parseInt(document.getElementById('length').value);
    const minUpper = parseInt(document.getElementById('upper').value);
    const minLower = parseInt(document.getElementById('lower').value);
    const minNumbers = parseInt(document.getElementById('numbers').value);
    const minSpecial = parseInt(document.getElementById('special').value);
    const avoidAmbiguous = document.getElementById('avoidAmbiguous').checked;

    const password = generatePassword(length, minUpper, minLower, minNumbers, minSpecial, avoidAmbiguous);
    document.getElementById('generatedPassword').textContent = password;
    document.getElementById('copyButton').style.display = 'block';
});

document.getElementById('copyButton').addEventListener('click', function () {
    const passwordText = document.getElementById('generatedPassword').textContent;
    navigator.clipboard.writeText(passwordText).then(() => {
        alert('Mot de passe copié dans le presse-papier !');
    });
});