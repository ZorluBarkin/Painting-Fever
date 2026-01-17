extends TextureButton
@export_category("Button Colors")
@export var hover_color: Color = Color.WHITE
@export var pressed_color: Color = Color.DARK_ORANGE
@export var normal_color: Color = Color.LIGHT_GRAY

@export_category("Label colors")
@export var label_hover_color: Color = Color.BLACK
@export var label_normal_color: Color = Color.WHITE

var _label: Label = null

func _ready():
	# get the label if there is one
	_get_label()
	normal_color = self_modulate
	mouse_entered.connect(_on_hover)
	mouse_exited.connect(_on_normal)
	button_down.connect(_on_pressed)
	button_up.connect(_on_hover)

func _get_label():
		# find label
	for child in find_children("*", "Label"):
		if child is Label:
			_label = child
			_label.self_modulate = label_normal_color
			break;

func _update_label_color(new_color: Color):
	if _label:
		_label.self_modulate = new_color

func _on_hover():
	self_modulate = hover_color
	_update_label_color(label_hover_color)

func _on_normal():
	self_modulate = normal_color
	_update_label_color(label_normal_color)
	
func _on_pressed():
	self_modulate = pressed_color
	_update_label_color(label_normal_color)
